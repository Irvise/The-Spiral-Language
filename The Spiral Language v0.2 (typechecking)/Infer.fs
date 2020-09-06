﻿module Spiral.Infer

open Spiral.Config

type [<ReferenceEquality>] 'a ref' = {mutable contents' : 'a}
type TT =
    | KindType
    | KindConstraint
    | KindFun of TT * TT
    | KindMetavar of TT option ref'

type Constraint =
    | CNumber
    | CPrototype of int

type [<ReferenceEquality>] Var = {
    scope : int
    constraints : Constraint Set // Must be stated up front and needs to be static in forall vars
    kind : TT // Is not supposed to have metavars.
    name : string // Is what gets printed.
    }

type [<ReferenceEquality>] MVar = {
    mutable scope : int
    mutable constraints : Constraint Set // Must be stated up front and needs to be static in forall vars
    kind : TT // Has metavars, and so is mutable.
    }

type Layout = Heap | HeapMutable

type TM =
    | TMText of string
    | TMVar of T

and T =
    | TyB
    | TyPrim of PrimitiveType
    | TySymbol of string
    | TyPair of T * T
    | TyRecord of Map<string, T>
    | TyFun of T * T
    | TyArray of T
    | TyHigherOrder of int * TT
    | TyApply of T * T * TT // Regular type functions (TyInl) get reduced, while this represents the staged reduction of nominals.
    | TyInl of Var * T
    | TyForall of Var * T
    | TyMetavar of MVar * T option ref
    | TyVar of Var
    | TyConstraint of Constraint
    | TyMacro of TM list
    | TyLayout of T * Layout

type TypeError =
    | KindError of TT * TT
    | KindError' of T * T
    | ExpectedSymbolAsRecordKey of T
    | UnboundVariable
    | UnboundModule
    | ModuleIndexFailedInOpen
    | ExpectedSymbolAsUnionKey
    | DuplicateKeyInUnion
    | TermError of T * T
    | ForallVarScopeError of string * T * T
    | ForallVarConstraintError of string * Constraint Set * Constraint Set
    | MetavarsNotAllowedInRecordWith
    | ExpectedLayout of T
    | ExpectedRecord of T
    | ExpectedRecordInsideALayout of T
    | ExpectedRecordAsResultOfIndex of T
    | RecordIndexFailed of string
    | ExpectedSymbol' of T
    | ExpectedSymbolInRecordWith of T
    | RealFunctionInTopDown
    | MissingRecordFieldsInPattern of T * string list
    | CasePatternNotFoundForType of int
    | CasePatternNotFound
    | CannotInferCasePatternFromTermInEnv of T
    | NominalInPatternUnbox of int
    | TypeInGlobalEnvIsNotNominal of T
    | UnionInPatternNominal of int
    | ConstraintError of Constraint * T
    | ExpectedAnnotation
    | ExpectedSinglePattern
    | RecursiveAnnotationHasMetavars of T
    | ValueRestriction of T
    | DuplicateRecInlName
    | ExpectedConstraint of T
    | InstanceNotFound of prototype: int * instance: int
    | ExpectedPrototype of T
    | ExpectedHigherOrder of T
    | InstanceArityError of prototype_arity: int * instance_arity: int
    | InstanceCoreVarsShouldMatchTheArityDifference of got: int * expected: int
    | InstanceKindError of TT * TT
    | KindNotAllowedInInstanceForall
    | InstanceVarShouldNotMatchAnyOfPrototypes
    | MissingBody

let shorten' x link next = let x = next x in link.contents' <- Some x; x
let rec visit_tt = function
    | KindMetavar({contents'=Some x} & link) -> shorten' x link visit_tt
    | a -> a

let shorten x link next = let x = next x in link := Some x; x
let rec visit_t = function
    | TyMetavar(_,{contents=Some x} & link) -> shorten x link visit_t
    | a -> a

open Spiral.BlockParsing
exception TypeErrorException of (Range * TypeError) list

let rec typevar = function
    | RawKindWildcard | RawKindStar -> KindType
    | RawKindFun(a,b) -> KindFun(typevar a, typevar b)

let rec metavars = function
    | RawTMacro _ | RawTVar _ | RawTTerm _ | RawTPrim _ | RawTWildcard _ | RawTB _ | RawTSymbol _ -> Set.empty
    | RawTMetaVar(_,a) -> Set.singleton a
    | RawTForall(_,_,a) | RawTArray(_,a) -> metavars a
    | RawTPair(_,a,b) | RawTApply(_,a,b) | RawTFun(_,a,b) -> metavars a + metavars b
    | RawTRecord(_,l) -> Map.fold (fun s _ v -> s + metavars v) Set.empty l

type HigherOrderCases =
    | HOCUnion of name: string * Var list * Map<string,T>
    | HOCNominal of name: string * Var list * T

open FSharpx.Collections
type TopEnv = {
    hoc : HigherOrderCases PersistentVector
    prototypes : {| instances : Map<int,Constraint Set list>; name : string; signature: T|} PersistentVector
    ty : Map<string,T>
    term : Map<string,T>
    }
type Env = { ty : Map<string,T>; term : Map<string,T> }

let kind_get x = 
    let rec loop = function
        | KindFun(a,b) -> a :: loop b
        | a -> [a]
    let l = loop x 
    {|arity=List.length l; args=l|}

let prototype_init_forall_kind = function
    | TyForall(a,b) -> a.kind
    | _ -> failwith "Compiler error: The prototype should have at least one forall."

let constraint_kind (env : TopEnv) x = 
    match x with
    | CNumber -> KindType 
    | CPrototype i -> prototype_init_forall_kind env.prototypes.[i].signature
    |> fun a -> KindFun(a, KindConstraint)

let constraint_name (env : TopEnv) = function
    | CNumber -> "number"
    | CPrototype i -> env.prototypes.[i].name

let rec tt env = function
    | TyHigherOrder(_,x) | TyApply(_,_,x) | TyMetavar({kind=x},_) | TyVar({kind=x}) -> x
    | TyLayout _ | TyMacro _ | TyB | TyPrim _ | TyForall _ | TyFun _ | TyRecord _ | TyPair _ | TySymbol _ | TyArray _ -> KindType
    | TyInl(v,a) -> KindFun(v.kind,tt env a)
    | TyConstraint x -> constraint_kind env x

let module_open (top_env : Env) (r : Range) b l =
    let tryFind env x =
        match Map.tryFind x env.term, Map.tryFind x env.ty with
        | Some (TyRecord a), Some (TyRecord b) -> ValueSome {term=a; ty=b}
        | _ -> ValueNone
    match tryFind top_env b with
    | ValueNone -> Error(r, UnboundModule)
    | ValueSome env ->
        let rec loop env = function
            | (r,x) :: x' ->
                match tryFind env x with
                | ValueSome env -> loop env x'
                | _ -> Error(r, ModuleIndexFailedInOpen)
            | [] -> Ok env
        loop env l

let validate_bound_vars (top_env : Env) term ty x =
    let errors = ResizeArray()
    let check_term term (a,b) = if Set.contains b term = false && Map.containsKey b top_env.term = false then errors.Add(a,UnboundVariable)
    let check_ty ty (a,b) = if Set.contains b ty = false && Map.containsKey b top_env.ty = false then errors.Add(a,UnboundVariable)
    let rec cterm term ty x =
        match x with
        | RawSymbolCreate _ | RawDefaultLit _ | RawLit _ | RawB _ -> ()
        | RawBigV(a,b) | RawV(a,b) -> check_term term (a,b)
        | RawType(_,x) -> ctype term ty x
        | RawMatch(_,body,l) -> cterm term ty body; List.iter (fun (a,b) -> cterm (cpattern term ty a) ty b) l
        | RawFun(_,l) -> List.iter (fun (a,b) -> cterm (cpattern term ty a) ty b) l
        | RawForall(_,(((_,(a,_)),l)),b) -> List.iter (check_ty ty) l; cterm term (Set.add a ty) b
        | RawRecBlock(_,l,on_succ) -> 
            let term = List.fold (fun s ((_,x),_) -> Set.add x s) term l
            List.iter (fun (_,x) -> cterm term ty x) l
            cterm term ty on_succ
        | RawRecordWith(_,a,b,c) ->
            List.iter (cterm term ty) a
            List.iter (function
                | RawRecordWithSymbol(_,e) | RawRecordWithSymbolModify(_,e) -> cterm term ty e
                | RawRecordWithInjectVar(v,e) | RawRecordWithInjectVarModify(v,e) -> check_term term v; cterm term ty e
                ) b
            List.iter (function RawRecordWithoutSymbol _ -> () | RawRecordWithoutInjectVar (a,b) -> check_term term (a,b)) c
        | RawOp(_,_,l) -> List.iter (cterm term ty) l
        | RawReal(_,x) | RawJoinPoint(_,x) -> cterm term ty x
        | RawAnnot(_,a,b) -> cterm term ty a; ctype term ty b
        | RawTypecase(_,a,b) -> 
            ctype term ty a
            List.iter (fun (a,b) -> 
                ctype term ty a
                cterm term (ty + metavars a) b
                ) b
        | RawModuleOpen(_,(a,b),l,on_succ) ->
            match module_open top_env a b l with
            | Ok x ->
                let combine e m = Map.fold (fun s k _ -> Set.add k s) e m
                cterm (combine term x.term) (combine ty x.ty) on_succ
            | Error e -> errors.Add(e)
        | RawHeapMutableSet(_,a,b) | RawSeq(_,a,b) | RawPairCreate(_,a,b) | RawIfThen(_,a,b) | RawApply(_,a,b) -> cterm term ty a; cterm term ty b
        | RawIfThenElse(_,a,b,c) -> cterm term ty a; cterm term ty b; cterm term ty c
        | RawMissingBody r -> errors.Add(r,MissingBody)
        | RawMacro(_,a) -> cmacro term ty a
    and cmacro term ty a =
        List.iter (function
            | RawMacroText _ -> ()
            | RawMacroTermVar(r,a) -> check_term term (r,a)
            | RawMacroTypeVar(r,a) -> check_term ty (r,a)
            ) a
    and ctype term ty x =
        match x with
        | RawTPrim _ | RawTWildcard _ | RawTB _ | RawTSymbol _ | RawTMetaVar _ -> ()
        | RawTVar(a,b) -> check_ty ty (a,b)
        | RawTPair(_,a,b) | RawTApply(_,a,b) | RawTFun(_,a,b) -> ctype term ty a; ctype term ty b
        | RawTRecord(_,l) -> Map.iter (fun _ -> ctype term ty) l
        | RawTForall(_,((_,(a,_)),l),b) -> List.iter (check_ty ty) l; ctype term (Set.add a ty) b
        | RawTArray(_,a) -> ctype term ty a
        | RawTTerm (_,a) -> cterm term ty a
        | RawTMacro(_,a) -> cmacro term ty a
    and cpattern term ty x =
        //let is_first = System.Collections.Generic.HashSet()
        let rec loop term x = 
            let f = loop term
            match x with
            | PatDefaultValue _ | PatValue _ | PatSymbol _ | PatB _ | PatE _ -> term
            | PatVar(_,b) -> 
                //if is_first.Add b then () // TODO: I am doing it like this so I can reuse this code later for variable highting.
                Set.add b term
            | PatDyn(_,x) | PatUnbox(_,x) -> f x
            | PatPair(_,a,b) -> loop (loop term a) b
            | PatRecordMembers(_,l) ->
                List.fold (fun s -> function
                    | PatRecordMembersSymbol(_,x) -> loop s x
                    | PatRecordMembersInjectVar((a,b),x) -> check_term term (a,b); loop s x
                    ) term l
            | PatActive(_,a,b) -> cterm term ty a; f b
            | PatAnd(_,a,b) | PatOr(_,a,b) -> loop (loop term a) b
            | PatAnnot(_,a,b) -> let r = f a in ctype r ty b; r 
            | PatWhen(_,a,b) -> let r = f a in cterm r ty b; r
            | PatNominal(_,(r,a),b) -> check_ty ty (r,a); f b
        loop term x

    match x with
    | Choice1Of2 x -> cterm term ty x
    | Choice2Of2 x -> ctype term ty x
    errors

let assert_bound_vars (top_env : Env) term ty x =
    let errors = validate_bound_vars top_env term ty x
    if 0 < errors.Count then raise (TypeErrorException (Seq.toList errors))

let rec subst (m : (Var * T) list) x =
    let f = subst m
    match x with
    | TyMetavar(_,{contents=Some x} & link) -> f x // Don't do path shortening here.
    | TyMetavar _ | TyConstraint _ | TyHigherOrder _ | TyB | TyPrim _ | TySymbol _ -> x
    | TyPair(a,b) -> TyPair(f a, f b)
    | TyRecord l -> TyRecord(Map.map (fun _ -> f) l)
    | TyFun(a,b) -> TyFun(f a, f b)
    | TyArray a -> TyArray(f a)
    | TyApply(a,b,c) -> TyApply(f a, f b, c)
    | TyVar a -> List.tryPick (fun (v,x) -> if a = v then Some x else None) m |> Option.defaultValue x
    | TyForall(a,b) -> TyForall(a, f b)
    | TyInl(a,b) -> TyInl(a, f b)
    | TyMacro a -> TyMacro(List.map (function TMVar x -> TMVar(f x) | x -> x) a)
    | TyLayout(a,b) -> TyLayout(f a,b)

let rec ho_split s = function 
    | TyApply(a,b,_) -> ho_split (b :: s) a 
    | TyHigherOrder _ as x -> x :: s
    | _ -> []

let rec constraint_process (env : TopEnv) (con,x') = 
    match con, visit_t x' with
    | con, TyMetavar(x,_) -> x.constraints <- Set.add con x.constraints; []
    | con, TyVar v & x -> if Set.contains con v.constraints then [] else [ConstraintError(con,x)]
    | CNumber, TyPrim (UInt8T | UInt16T | UInt32T | UInt64T | Int8T | Int16T | Int32T | Int64T | Float32T | Float64T) -> []
    | CPrototype i & con, x ->
        match ho_split [] x with
        | [] -> [ConstraintError(con,x)]
        | TyHigherOrder(i',_) :: x' ->
            match Map.tryFind i' env.prototypes.[i].instances with
            | Some cons -> 
                let rec loop ers = function
                    | con :: con', x :: x' -> loop (List.append (constraints_process env (con,x)) ers) (con',x')
                    | [], _ -> ers
                    | _, [] -> failwith "Compiler error: The number of constraints for higher order type should never be more than its arity."
                loop [] (cons,x')
            | None -> [InstanceNotFound(i,i')]
        | _ :: _ -> failwith "Compiler error: The first item of a ho_split should always be a higher order type."
    | con, x -> [ConstraintError(con,x)]
and constraints_process env (con,b) = 
    match visit_t b with
    | TyVar b -> if con.IsSubsetOf b.constraints = false then [ForallVarConstraintError(b.name,con,b.constraints)] else []
    | b -> Set.fold (fun ers con -> List.append (constraint_process env (con, b)) ers) [] con

let rec kind_subst = function
    | KindMetavar ({contents'=Some x} & link) -> shorten' x link kind_subst
    | KindMetavar _ | KindConstraint | KindType as x -> x
    | KindFun(a,b) -> KindFun(kind_subst a,kind_subst b)

let rec term_subst = function
    | TyMetavar(_,{contents=Some x} & link) -> shorten x link term_subst
    | TyConstraint _ | TyMetavar _ | TyVar _ | TyHigherOrder _ | TyB | TyPrim _ | TySymbol _ as x -> x
    | TyPair(a,b) -> TyPair(term_subst a, term_subst b)
    | TyRecord l -> TyRecord(Map.map (fun _ -> term_subst) l)
    | TyFun(a,b) -> TyFun(term_subst a, term_subst b)
    | TyForall(a,b) -> TyForall(a,term_subst b)
    | TyArray a -> TyArray(term_subst a)
    | TyApply(a,b,c) -> TyApply(term_subst a, term_subst b, c)
    | TyInl(a,b) -> TyInl(a,term_subst b)
    | TyMacro a -> TyMacro(List.map (function TMVar x -> TMVar(term_subst x) | x -> x) a)
    | TyLayout(a,b) -> TyLayout(term_subst a,b)

let rec foralls_get = function
    | RawForall(_,a,b) -> let a', b = foralls_get b in a :: a', b
    | b -> [], b

let rec foralls_ty_get = function
    | TyForall(a,b) -> let a', b = foralls_ty_get b in a :: a', b
    | b -> [], b

let rec kind_force = function
    | KindMetavar ({contents'=Some x} & link) -> shorten' x link kind_subst
    | KindMetavar link -> let x = KindType in link.contents' <- Some x; x
    | KindConstraint | KindType as x -> x
    | KindFun(a,b) -> KindFun(kind_subst a,kind_subst b)

let rec has_metavars x =
    let f = has_metavars
    match visit_t x with
    | TyMetavar _ -> true
    | TyConstraint | TyVar _ | TyHigherOrder _ | TyB | TyPrim _ | TySymbol _ -> false
    | TyLayout(a,_) | TyForall(_,a) | TyInl(_,a) | TyArray a -> f a
    | TyApply(a,b,_) | TyFun(a,b) | TyPair(a,b) -> f a || f b
    | TyRecord l -> Map.exists (fun _ -> f) l
    | TyMacro a -> List.exists (function TMVar x -> has_metavars x | _ -> false) a

let show_primt = function
    | UInt8T -> "u8"
    | UInt16T -> "u16"
    | UInt32T -> "u32"
    | UInt64T -> "u64"
    | Int8T -> "i8"
    | Int16T -> "i16"
    | Int32T -> "i32"
    | Int64T -> "i64"
    | Float32T -> "f32"
    | Float64T -> "f64"
    | BoolT -> "bool"
    | StringT -> "string"
    | CharT -> "char"

let p prec prec' x = if prec < prec' then x else sprintf "(%s)" x
let show_kind x =
    let rec f prec x =
        let p = p prec
        match x with
        | KindMetavar {contents'=Some x} -> f prec x
        | KindMetavar _ -> "?"
        | KindType -> "*"
        | KindConstraint -> "/"
        | KindFun(a,b) -> p 20 (sprintf "%s -> %s" (f 20 a) (f 19 b))
    f -1 x

let show_constraint prec (env : TopEnv) x = p prec 0 (sprintf "%s : %s" (constraint_name env x) (constraint_kind env x |> show_kind))
let show_constraints env x = Set.toList x |> List.map (constraint_name env) |> String.concat "; " |> sprintf "{%s}"

let show_hoc (env : TopEnv) i = match env.hoc.[i] with HOCNominal(name,_,_) | HOCUnion(name,_,_) -> name

let show_t (env : TopEnv) x =
    let show_var (a : Var) =
        let n = match a.kind with KindType -> a.name | _ -> sprintf "(%s : %s)" a.name (show_kind a.kind)
        if Set.isEmpty a.constraints then n
        else sprintf "%s %s" n (show_constraints env a.constraints)
    let rec f prec x =
        let p = p prec
        match x with
        | TyMetavar(_,{contents=Some x}) -> f prec x
        | TyMetavar _ -> "?"
        | TyVar a -> a.name
        | TyHigherOrder(i,_) -> show_hoc env i
        | TyB -> "()"
        | TyPrim x -> show_primt x
        | TySymbol x -> sprintf ".%s" x
        | TyForall _ -> 
            let a, b =
                let rec loop = function
                    | TyForall(a,b) -> let a',b = loop b in (a :: a'), b
                    | b -> [], b
                loop x
            let a = List.map show_var a |> String.concat " "
            p 0 (sprintf "forall %s. %s" a (f -1 b))
        | TyInl _ -> 
            let a, b =
                let rec loop = function
                    | TyInl(a,b) -> let a',b = loop b in (a :: a'), b
                    | b -> [], b
                loop x
            let a = List.map show_var a |> String.concat " "
            p 0 (sprintf "%s => %s" a (f -1 b))
        | TyArray a -> p 30 (sprintf "array %s" (f 30 a))
        | TyApply(a,b,_) -> p 30 (sprintf "%s %s" (f 29 a) (f 30 b))
        | TyPair(a,b) -> 
            match visit_t a, visit_t b with
            | TySymbol a, b when 0 < a.Length && System.Char.IsLower(a,0) && a.[a.Length-1] = '_' -> 
                let show (s,a) = sprintf "%s: %s" s (f 15 a)
                let rec loop (a,b) = 
                    match a,b with
                    | s :: s', TyPair(a,b) -> show (s,a) :: loop (s',b)
                    | s :: [], a -> [show (s,a)]
                    | s, a -> [show (String.concat "_" s, a)]
                p 15 (loop (a.Split('_',System.StringSplitOptions.RemoveEmptyEntries) |> (fun x -> x.[0] <- to_upper x.[0]; Array.toList x), b) |> String.concat " ")
            | TySymbol a, TyB when 0 < a.Length && System.Char.IsLower(a,0) -> to_upper a
            | a,b -> p 25 (sprintf "%s, %s" (f 25 a) (f 24 b))
        | TyFun(a,b) -> p 20 (sprintf "%s -> %s" (f 20 a) (f 19 b))
        | TyRecord l -> sprintf "{%s}" (l |> Map.toList |> List.map (fun (k,v) -> sprintf "%s : %s" k (f -1 v)) |> String.concat "; ")
        | TyConstraint a -> show_constraint prec env a
        | TyMacro a -> p 30 (List.map (function TMVar a -> f -1 a | TMText a -> a) a |> String.concat "")
        | TyLayout(a,b) -> 
            let b = match b with Heap -> "heap" | HeapMutable -> "mut"
            p 30 (sprintf "%s %s" b (f 30 a))

    f -1 x

let show_type_error (env : TopEnv) x =
    let f = show_t env
    match x with
    | ExpectedLayout a -> sprintf "Expected a layout type.\nGot: %s" (f a)
    | ExpectedSymbol' a -> sprintf "Expected a symbol.\nGot: %s" (f a)
    | KindError(a,b) -> sprintf "Kind unification failure.\nGot:      %s\nExpected: %s" (show_kind a) (show_kind b)
    | KindError'(a,b) -> sprintf "Kind unification failure.\nGot:      %s\nExpected: %s" (f a) (f b)
    | TermError(a,b) -> sprintf "Unification failure.\nGot:      %s\nExpected: %s" (f a) (f b)
    | ExpectedSymbolAsRecordKey a -> sprintf "Expected symbol as a record key.\nGot: %s" (f a)
    | UnboundVariable -> sprintf "Unbound variable."
    | UnboundModule -> sprintf "Unbound module."
    | ModuleIndexFailedInOpen -> sprintf "Module does not have a submodule with that key."
    | ExpectedSymbolAsUnionKey -> sprintf "Expected a symbol as the union key."
    | DuplicateKeyInUnion -> sprintf "Union cases must have unique keys."
    | ForallVarScopeError(a,_,_) -> sprintf "Tried to unify the forall variable %s with a metavar outside its scope." a
    | ForallVarConstraintError(n,a,b) -> sprintf "Metavariable's constraints must be a subset of the forall var %s's.\nGot: %s\nExpected: %s" n (show_constraints env a) (show_constraints env b)
    | MetavarsNotAllowedInRecordWith -> sprintf "In the top-down segment the record keys need to be fully known. Please add an annotation."
    | ExpectedRecord a -> sprintf "Expected a record.\nGot: %s" (f a)
    | ExpectedRecordInsideALayout a -> sprintf "Expected a record inside a layout type.\nGot: %s" (f a)
    | ExpectedRecordAsResultOfIndex a -> sprintf "Expected a record as result of index.\nGot: %s" (f a)
    | RecordIndexFailed a -> sprintf "The record does not have the key: %s" a
    | ExpectedSymbolInRecordWith a -> sprintf "Expected a symbol.\nGot: %s" (f a)
    | RealFunctionInTopDown -> sprintf "Real segment functions are forbidden in the top-down segment."
    | MissingRecordFieldsInPattern(a,b) -> sprintf "The record is missing the following fields: %s.\nGot: %s" (String.concat ", " b) (f a)
    | CasePatternNotFoundForType i -> sprintf "%s does not have this case." (show_hoc env i)
    | CasePatternNotFound -> "Cannot find a function with the same name as this case in the environment."
    | CannotInferCasePatternFromTermInEnv a -> sprintf "Cannot infer the higher order type that has this case from the following type.\nGot: %s" (f a)
    | NominalInPatternUnbox i -> sprintf "Expected an union type, but %s is a nominal." (show_hoc env i)
    | TypeInGlobalEnvIsNotNominal a -> sprintf "Expected a nominal type.\nGot: %s" (f a)
    | UnionInPatternNominal i -> sprintf "Expected a nominal type, but %s is an union." (show_hoc env i)
    | ConstraintError(a,b) -> sprintf "Constraint satisfaction error.\nGot: %s\nFails to satisfy: %s" (f b) (constraint_name env a)
    | ExpectedAnnotation -> sprintf "Recursive functions with foralls must be fully annotated."
    | ExpectedSinglePattern -> sprintf "Recursive functions with foralls must not have multiple clauses in their patterns."
    | RecursiveAnnotationHasMetavars a -> sprintf "Recursive functions with foralls must not have metavars.\nGot: %s" (f a)
    | ValueRestriction a -> sprintf "Metavars that are not part of the enclosing function's signature are not allowed. They need to be values.\nGot: %s" (f a)
    | DuplicateRecInlName -> "Shadowing of functions by the members of the same mutually recursive block is not allowed."
    | ExpectedConstraint a -> sprintf "Expected a constraint.\nGot: %s" (f a)
    | InstanceNotFound(prot,ins) -> sprintf "The higher order type %s does not have the prototype instance for %s." (show_hoc env prot) env.prototypes.[ins].name
    | ExpectedPrototype a -> sprintf "Expected a prototype.\nGot: %s" (f a)
    | ExpectedHigherOrder a -> sprintf "Expected a higher order type.\nGot: %s" (f a)
    | InstanceArityError(prot,ins) -> sprintf "The arity of the instance must be greater or equal to that of the prototype.\nInstance arity:  %i\nPrototype arity: %i" ins prot
    | InstanceCoreVarsShouldMatchTheArityDifference(num_vars,expected) -> sprintf "The number of forall variables in the instance needs to be specified so it equals the difference between the instance arity and the prototype arity.\nInstance variables specified: %i\nExpected:                     %i" num_vars expected
    | InstanceKindError(a,b) -> sprintf "The kinds of the instance foralls are incompatible with those of the prototype.\nGot:      %s\nExpected: %s" (show_kind a) (show_kind b)
    | KindNotAllowedInInstanceForall -> "Kinds should not be explicitly stated in instance foralls."
    | InstanceVarShouldNotMatchAnyOfPrototypes -> "Instance forall must not have the same name as any of the prototype foralls."
    | MissingBody -> "The function body is missing."

let loc_env (x : TopEnv) = {term=x.term; ty=x.ty}
let names_of vars = List.map (fun x -> x.name) vars |> Set

open Spiral.Tokenize
let lit = function
    | LitUInt8 _ -> TyPrim UInt8T
    | LitUInt16 _ -> TyPrim UInt16T
    | LitUInt32 _ -> TyPrim UInt32T
    | LitUInt64 _ -> TyPrim UInt64T
    | LitInt8 _ -> TyPrim Int8T
    | LitInt16 _ -> TyPrim Int16T
    | LitInt32 _ -> TyPrim Int32T
    | LitInt64 _ -> TyPrim Int64T
    | LitFloat32 _ -> TyPrim Float32T
    | LitFloat64 _ -> TyPrim Float64T
    | LitBool _ -> TyPrim BoolT
    | LitString _ -> TyPrim StringT
    | LitChar _ -> TyPrim CharT

// TODO: Extend this later.
let rec strip_fun_pat x = 
    x |> List.map (function
        | PatAnnot(_,x,_), body -> x, strip_annotations body
        | PatDyn(r,PatAnnot(_,x,_)),body -> PatDyn(r,x), strip_annotations body
        | x -> x
        )

and strip_annotations = function
    | RawFun(r,l) -> RawFun(r,strip_fun_pat l)
    | RawJoinPoint(r, RawAnnot(_,x,_)) -> RawJoinPoint(r,x)
    | RawAnnot(_,x,_) -> x
    | x -> x

let hovars (x : HoVar list) = 
    List.mapFold (fun s (_,(n,t)) -> 
        let v = {scope=0; kind=typevar t; name=n; constraints=Set.empty}
        v, Map.add n (TyVar v) s
        ) Map.empty x

let autogen_name (i : int) = let x = char i + 'a' in if 'z' < x then sprintf "'%i" i else sprintf "'%c" x

open Spiral.TypecheckingUtils
open System.Collections.Generic
let infer (top_env' : TopEnv) expr =
    let hoc = top_env'.hoc
    let top_env = loc_env top_env'
    let errors = ResizeArray()
    let scope = ref 0
    let autogened_forallvar_count = ref 0

    let hover_types = Dictionary(HashIdentity.Reference)
    let type_application = Dictionary(HashIdentity.Reference)

    let fresh_kind () = KindMetavar {contents'=None}
    let fresh_var'' x = TyMetavar (x, ref None)
    let fresh_var' kind = fresh_var'' {scope= !scope; constraints=Set.empty; kind=kind}
    let fresh_subst_var cons kind = fresh_var'' {scope= !scope; constraints=cons; kind=kind}
    let forall_subst_all x =
        let rec loop m x = 
            match visit_t x with
            | TyForall(a,b) -> loop ((a, fresh_subst_var a.constraints a.kind) :: m) b
            | x -> if List.isEmpty m then x else subst m x
        loop [] x

    let generalize (forall_vars : Var list) (body : T) =
        let scope = !scope
        let h = HashSet(HashIdentity.Reference)
        List.iter (h.Add >> ignore) forall_vars
        let generalized_metavars = ResizeArray()
        let rec replace_metavars x =
            let f = replace_metavars
            match x with
            | TyMetavar(_,{contents=Some x} & link) -> shorten x link f
            | TyMetavar(x, link) when scope = x.scope ->
                let v = TyVar {scope=x.scope; constraints=x.constraints; kind=kind_force x.kind; name=autogen_name !autogened_forallvar_count}
                incr autogened_forallvar_count
                link := Some v
                replace_metavars v
            | TyVar v -> (if h.Add(v) then generalized_metavars.Add(v)); x
            | TyConstraint _ | TyMetavar _ | TyHigherOrder _ | TyB | TyPrim _ | TySymbol _ as x -> x
            | TyPair(a,b) -> TyPair(f a, f b)
            | TyRecord l -> TyRecord(Map.map (fun _ -> f) l)
            | TyFun(a,b) -> TyFun(f a, f b)
            | TyForall(a,b) -> TyForall(a,f b)
            | TyArray a -> TyArray(f a)
            | TyApply(a,b,c) -> TyApply(f a,f b,c)
            | TyInl(a,b) -> TyInl(a,f b)
            | TyMacro a -> TyMacro(List.map (function TMVar a -> TMVar(f a) | a -> a) a)
            | TyLayout(a,b) -> TyLayout(f a,b)

        let f x s = TyForall(x,s)
        Seq.foldBack f generalized_metavars (replace_metavars body)
        |> List.foldBack f forall_vars 

    let inline unify_kind' er (got : TT) (expected : TT) : unit =
        let rec loop (a'',b'') =
            match visit_tt a'', visit_tt b'' with
            | KindType, KindType
            | KindConstraint, KindConstraint -> ()
            | KindFun(a,a'), KindFun(b,b') -> loop (a,b); loop (a',b')
            | KindMetavar a, KindMetavar b & b' -> if a <> b then a.contents' <- Some b'
            | KindMetavar link, b | b, KindMetavar link -> link.contents' <- Some b
            | _ -> er()
        loop (got, expected)
    let unify_kind r got expected = 
        try unify_kind' (fun () -> raise (TypeErrorException [r, KindError (got, expected)])) got expected
        with :? TypeErrorException as e -> errors.AddRange e.Data0

    let unify (r : Range) (got : T) (expected : T) : unit =
        let unify_kind = unify_kind' (fun () -> raise (TypeErrorException [r, KindError' (got, expected)]))
        let er () = raise (TypeErrorException [r, TermError(got, expected)])

        // Does occurs checking.
        // Does scope checking in forall vars.
        let rec validate_unification i x =
            let f = validate_unification i
            match visit_t x with
            | TyMacro _ | TyHigherOrder _ | TyB | TyPrim _ | TySymbol _ -> ()
            | TyForall(_,a) | TyInl(_,a) | TyArray a -> f a
            | TyApply(a,b,_) | TyFun(a,b) | TyPair(a,b) -> f a; f b
            | TyRecord l -> Map.iter (fun _ -> f) l
            | TyVar b -> if i.scope < b.scope then raise (TypeErrorException [r,ForallVarScopeError(b.name,got,expected)])
            | TyMetavar(x,_) -> if i = x then er() elif i.scope < x.scope then x.scope <- i.scope
            | TyConstraint a -> unify_kind i.kind (constraint_kind top_env' a)
            | TyLayout(a,_) -> f a

        let rec loop (a'',b'') = 
            match visit_t a'', visit_t b'' with
            | TyMetavar(a,link), TyMetavar(b,_) & b' ->
                if a <> b then
                    unify_kind a.kind b.kind
                    b.scope <- min a.scope b.scope
                    b.constraints <- a.constraints + b.constraints
                    link := Some b'
            | (TyMetavar(a,link), b | b, TyMetavar(a,link)) ->
                validate_unification a b
                unify_kind a.kind (tt top_env' b)
                match constraints_process top_env' (a.constraints,b) with
                | [] -> link := Some b
                | constraint_errors -> raise (TypeErrorException (List.map (fun x -> r,x) constraint_errors))
            | TyVar a, TyVar b when a = b -> ()
            | (TyPair(a,a'), TyPair(b,b') | TyFun(a,a'), TyFun(b,b') | TyApply(a,a',_), TyApply(b,b',_)) -> loop (a,b); loop (a',b')
            | TyRecord l, TyRecord l' ->
                let a,b = Map.toArray l, Map.toArray l'
                if a.Length <> b.Length then er ()
                else Array.iter2 (fun (_,a) (_,b) -> loop (a,b)) a b
            | TyHigherOrder(i,_), TyHigherOrder(i',_) when i = i' -> ()
            | TyB, TyB -> ()
            | TyPrim x, TyPrim x' when x = x' -> ()
            | TySymbol x, TySymbol x' when x = x' -> ()
            | TyArray a, TyArray b -> loop (a,b)
            // Note: Unifying these two only makes sense if the expected is fully inferred already.
            | TyForall(a,b), TyForall(a',b') | TyInl(a,b), TyInl(a',b') when a.kind = a'.kind && a.constraints = a'.constraints -> loop (b, subst [a',TyVar a] b')
            | TyMacro a, TyMacro b -> 
                List.iter2 (fun a b -> 
                    match a,b with
                    | TMText a, TMText b when System.Object.ReferenceEquals(a,b) || a = b -> ()
                    | TMVar a, TMVar b -> loop(a,b)
                    | _ -> er ()
                    ) a b
            | TyLayout(a,a'), TyLayout(b,b') when a' = b' -> loop (a,b)
            | _ -> er ()

        try loop (got, expected)
        with :? TypeErrorException as e -> errors.AddRange e.Data0

    let rec apply_record r s l x =
        let f = apply_record r s
        match visit_t x with
        | TySymbol x ->
            match Map.tryFind x l with
            | Some x -> unify r s x
            | None -> errors.Add(r,RecordIndexFailed x)
        | TyPair(TySymbol x, b) ->
            match Map.tryFind x l with
            | Some (TyRecord l) -> f l b
            | Some a -> unify r a (TyFun(b,s))
            | None -> errors.Add(r,RecordIndexFailed x)
        | x -> errors.Add(r,ExpectedSymbolAsRecordKey x)
           
    let assert_bound_vars env a =
        let keys_of m = Map.fold (fun s k _ -> Set.add k s) Set.empty m
        validate_bound_vars top_env (keys_of env.term) (keys_of env.ty) (Choice1Of2 a) |> errors.AddRange

    let fresh_var () = fresh_var' KindType

    let v env top_env a = Map.tryFind a env |> Option.orElseWith (fun () -> Map.tryFind a top_env) |> Option.map visit_t
    let v_term env a = v env.term top_env.term a
    let v_ty env a = v env.ty top_env.ty a
    let typevar_to_var ty (((_,(name,kind)),constraints) : TypeVar) : Var = 
        let rec typevar = function
            | RawKindWildcard -> fresh_kind()
            | RawKindStar -> KindType
            | RawKindFun(a,b) -> KindFun(typevar a, typevar b)
        let kind = typevar kind
        let cons =
            constraints |> List.choose (fun (r,x) ->
                match v ty top_env.ty x with
                | Some (TyConstraint x & a) -> hover_types.Add(r,a); unify_kind r (KindFun(kind,KindConstraint)) (constraint_kind top_env' x); Some x
                | Some x -> errors.Add(r,ExpectedConstraint x); None
                | None -> errors.Add(r,UnboundVariable); None
                ) |> Set.ofList
        {scope= !scope; constraints=cons; kind=kind_force kind; name=name}

    let typevars ty (l : TypeVar list) =
        List.mapFold (fun s x ->
            let v = typevar_to_var s x
            v, Map.add v.name (TyVar v) s
            ) ty l

    let rec term (env : Env) s x =
        let f = term env
        let f' x = let v = fresh_var() in f v x; visit_t v
        let inline rawv (r,a) on_succ =
            match v_term env a with
            | None -> errors.Add(r,UnboundVariable)
            | Some (TySymbol "<real>") -> errors.Add(r,RealFunctionInTopDown)
            | Some a -> 
                match a with TyForall _ -> type_application.Add(x,s) | _ -> ()
                hover_types.Add(r,s); on_succ (forall_subst_all a)
        match x with
        | RawB r -> unify r s TyB
        | RawV(r,a) -> rawv (r,a) (unify r s)
        | RawBigV(r,a) -> rawv (r,a) (unify r (TyFun(TyB,s)))
        | RawDefaultLit(r,_) -> type_application.Add(x,s); hover_types.Add(r,s); unify r s (fresh_subst_var (Set.singleton CNumber) KindType)
        | RawLit(r,a) -> unify r s (lit a)
        | RawSymbolCreate(r,x) -> unify r s (TySymbol x)
        | RawType(_,x) -> ty env s x
        | RawIfThenElse(_,cond,tr,fl) -> f (TyPrim BoolT) cond; f s tr; f s fl
        | RawIfThen(r,cond,tr) -> f (TyPrim BoolT) cond; unify r s TyB; f TyB tr
        | RawPairCreate(r,a,b) ->
            let q,w = fresh_var(), fresh_var()
            unify r s (TyPair(q, w))
            f q a; f w b
        | RawSeq(_,a,b) -> f TyB a; f s b
        | RawReal(_,a) -> assert_bound_vars env a
        | RawOp(_,_,l) -> List.iter (assert_bound_vars env) l
        | RawJoinPoint(_,a) -> f s a
        | RawApply(r,a',b) ->
            match f' a' with
            | TyLayout(a,_) ->
                match visit_t a with
                | TyRecord l -> apply_record r s l (f' b)
                | a -> errors.Add(r,ExpectedRecordInsideALayout a)
            | TyRecord l -> apply_record r s l (f' b)
            | a -> let v = fresh_var() in unify (range_of_expr a') a (TyFun(v,s)); f v b
        | RawAnnot(_,a,b) -> ty env s b; f s a
        | RawModuleOpen(_,(a,b),l,on_succ) ->
            match module_open top_env a b l with
            | Ok x ->
                let combine e m = Map.foldBack Map.add m e
                term {term = combine env.term x.term; ty = combine env.ty x.ty} s on_succ
            | Error e -> errors.Add(e)
        | RawRecordWith(r,l,withs,withouts) ->
            let i = errors.Count
            let er_metavar () = raise (TypeErrorException (if errors.Count = i then [range_of_expr x, MetavarsNotAllowedInRecordWith] else []))
            let record x =
                match f' x with
                | TyRecord m -> m
                | TyMetavar _ -> er_metavar()
                | a -> raise (TypeErrorException [range_of_expr x, ExpectedRecord a])
            let symbol x =
                match f' x with
                | TySymbol x -> x
                | TyMetavar _ -> er_metavar()
                | a -> raise (TypeErrorException [range_of_expr x, ExpectedSymbolInRecordWith a])
            let tc (l,m) =
                let m =
                    List.fold (fun m x -> 
                        let with_symbol ((_,a),b) = 
                            let v = fresh_var()
                            f v b
                            Map.add a v m
                        let with_symbol_modify ((r,a),b) =
                            let x = Map.tryFind a m |> Option.defaultWith (fun () -> errors.Add(r,RecordIndexFailed a); fresh_var())
                            let v = fresh_var()
                            f (TyFun(x,v)) b
                            Map.add a v m
                        let inline with_inject next ((r,a),b) =
                            match v_term env a with
                            | Some (TySymbol a as x) -> hover_types.Add(r,x); next ((r,a),b)
                            | Some x -> errors.Add(r, ExpectedSymbolAsRecordKey x); m
                            | None -> errors.Add(r, UnboundVariable); m
                        match x with
                        | RawRecordWithSymbol(a,b) -> with_symbol (a,b)
                        | RawRecordWithSymbolModify(a,b) -> with_symbol_modify (a,b)
                        | RawRecordWithInjectVar(a,b) -> with_inject with_symbol (a,b)
                        | RawRecordWithInjectVarModify(a,b) -> with_inject with_symbol_modify (a,b)
                        ) m withs
                let m =
                    List.fold (fun m -> function
                        | RawRecordWithoutSymbol(_,a) -> Map.remove a m
                        | RawRecordWithoutInjectVar(r,a) ->
                            match v_term env a with
                            | Some (TySymbol a as x) -> hover_types.Add(r,x); Map.remove a m
                            | Some x -> errors.Add(r, ExpectedSymbolAsRecordKey x); m
                            | None -> errors.Add(r, UnboundVariable); m
                        ) m withouts
                    |> TyRecord |> List.foldBack (fun (m,a) m' -> Map.add a m' m |> TyRecord) l
                if i = errors.Count then unify r s m
            try match l with
                | x :: x' ->
                    List.mapFold (fun m x ->
                        let sym = symbol x
                        match Map.tryFind sym m |> Option.map visit_t with
                        | Some (TyRecord m') -> (m,sym), m'
                        | Some a -> raise (TypeErrorException [range_of_expr x, ExpectedRecordAsResultOfIndex a])
                        | None -> raise (TypeErrorException [range_of_expr x, RecordIndexFailed sym])
                        ) (record x) x'
                | [] -> [], Map.empty
            with :? TypeErrorException as e -> errors.AddRange e.Data0; [], Map.empty
            |> tc
        | RawFun(r,l) ->
            let q,w = fresh_var(), fresh_var()
            unify r s (TyFun(q,w))
            List.iter (fun (a,b) -> term (pattern env q a) w b) l
        | RawForall _ -> failwith "Compiler error: Should be handled in let statements."
        | RawMatch(_,(RawForall _ | RawFun _) & body,[PatVar(r,name), on_succ]) -> term (inl env ((r, name), body)) s on_succ
        | RawRecBlock(_,l',on_succ) -> term (rec_block env l') s on_succ
        | RawMatch(_,body,l) ->
            let body_var = fresh_var()
            f body_var body
            let l = List.map (fun (a,on_succ) -> pattern env body_var a, on_succ) l
            List.iter (fun (env,on_succ) -> term env s on_succ) l
        | RawMissingBody r -> errors.Add(r,MissingBody)
        | RawMacro(_,a) ->
            let f r = function Some a -> hover_types.Add(r,a) | None -> errors.Add(r,UnboundVariable)
            List.iter (function
                | RawMacroText _ -> ()
                | RawMacroTermVar(r,a) -> v_term env a |> f r
                | RawMacroTypeVar(r,a) -> v_ty env a |> f r
                ) a
        | RawHeapMutableSet(r,a,b) ->
            let rec loop = function
                | RawApply(r,a',b') ->
                    match loop a' |> visit_t with
                    | TyRecord a ->
                        match f' b' with
                        | TySymbol b ->
                            match Map.tryFind b a with
                            | Some x -> x
                            | _ -> raise (TypeErrorException [r, RecordIndexFailed b])
                        | b -> raise (TypeErrorException [range_of_expr b', ExpectedSymbol' b])
                    | a -> raise (TypeErrorException [range_of_expr a', ExpectedRecord a])
                | a' ->
                    match f' a' with
                    | TyLayout(a,_) -> a
                    | a -> raise (TypeErrorException [range_of_expr a', ExpectedLayout a])

            unify r s TyB
            f (loop a) b
        | RawTypecase _ -> failwith "Compiler error: `typecase` should not appear in the top down segment."
    and inl env ((r, name), body) =
        incr scope
        let vars,body = foralls_get body
        let vars,env_ty = typevars env.ty vars
        let body_var = fresh_var()
        term {env with ty = env_ty} body_var body
        let t = generalize vars body_var
        hover_types.Add(r,t)
        let env = {env with term = Map.add name t env.term }
        decr scope
        env
    and rec_block env l' =
        incr scope
        let env =
            let has_foralls = List.exists (function (_,RawForall _) -> true | _ -> false) l'
            if has_foralls then
                let i = errors.Count
                let l,m =
                    List.mapFold (fun s ((r,name),body) ->
                        let vars,body = foralls_get body
                        let vars, env_ty = typevars env.ty vars
                        let body_var = term_annotations {env with ty = env_ty} body
                        let term env = term {env with ty = env_ty} body_var (strip_annotations body)
                        let ty = List.foldBack (fun x s -> TyForall(x,s)) vars body_var
                        hover_types.Add(r,ty)
                        term, Map.add name ty s
                        ) env.term l'
                
                if errors.Count = i then
                    l |> List.iter ((|>) {env with term = m})
                    {env with term = m}
                else
                    List.fold (fun s ((_,name),_) -> 
                        let v = {scope= !scope; constraints=Set.empty; kind=KindType; name="x"}
                        Map.add name (TyForall(v, TyVar v)) s
                        ) env.term l'
                    |> fun term -> {env with term = term}
            else 
                let l, m = 
                    List.mapFold (fun s ((r,name),body) -> 
                        let body_var = fresh_var()
                        let term env = term env body_var body
                        let gen env : Env = 
                            let t = generalize [] body_var
                            hover_types.Add(r,t)
                            {env with term = Map.add name t env.term}
                        (term, gen), Map.add name body_var s
                        ) env.term l'
                let _ =
                    let env = {env with term=m}
                    List.iter (fun (term, _) -> term env) l
                List.fold (fun env (_,gen) -> gen env) env l
        decr scope
        env
    and term_annotations env x =
        let f t = 
            let i = errors.Count
            let v = fresh_var()
            ty env v t
            let v = term_subst v
            if i = errors.Count && has_metavars v then errors.Add(range_of_texpr t, RecursiveAnnotationHasMetavars v)
            v
        match x with
        | RawFun(_,[(PatAnnot(_,_,t) | PatDyn(_,PatAnnot(_,_,t))),body]) -> TyFun(f t, term_annotations env body)
        | RawFun(_,[pat,body]) -> errors.Add(range_of_pattern pat, ExpectedAnnotation); TyFun(fresh_var(), term_annotations env body)
        | RawFun(r,_) -> errors.Add(r, ExpectedSinglePattern); TyFun(fresh_var(), fresh_var())
        | RawJoinPoint(_, RawAnnot(_,_,t)) | RawAnnot(_,_,t) -> f t
        | x -> errors.Add(range_of_expr x,ExpectedAnnotation); fresh_var()
    and ty (env : Env) s x =
        let f s x = ty env s x
        match x with
        | RawTWildcard r -> hover_types.Add(r,s)
        | RawTVar(r,x) -> 
            match v_ty env x with
            | Some x -> hover_types.Add(r,x); unify r s x
            | None -> errors.Add(r, UnboundVariable)
        | RawTB r -> unify r s TyB
        | RawTSymbol(r,x) -> unify r s (TySymbol x)
        | RawTPrim(r,x) -> unify r s (TyPrim x)
        | RawTArray(r,x) -> let v = fresh_var() in unify r s (TyArray v); f v x
        | RawTPair(r,a,b) -> 
            let q,w = fresh_var(), fresh_var()
            unify r s (TyPair(q,w))
            f q a; f w b
        | RawTFun(r,a,b) -> 
            let q,w = fresh_var(), fresh_var()
            unify r s (TyFun(q,w))
            f q a; f w b
        | RawTRecord(r,l) -> 
            let l' = Map.map (fun _ _ -> fresh_var()) l
            unify r s (TyRecord l')
            Map.iter (fun k s -> f s l.[k]) l'
        | RawTForall(r,a,b) ->
            let a = typevar_to_var env.ty a
            let body_var = fresh_var()
            ty {env with ty = Map.add a.name (TyVar a) env.ty} body_var b
            unify r s (TyForall(a, body_var))
        | RawTApply(r,a',b) ->
            let f' k x = let v = fresh_var' k in f v x; visit_t v
            match f' (fresh_kind()) a' with
            | TyRecord l -> 
                match f' KindType b with
                | TySymbol x ->
                    match Map.tryFind x l with
                    | Some x -> unify r s x
                    | None -> errors.Add(r,RecordIndexFailed x)
                | b -> errors.Add(r,ExpectedSymbolAsRecordKey b)
            | TyInl(a,body) -> let v = fresh_var' a.kind in f v b; unify r s (subst [a,v] body)
            | a -> 
                let q,w = fresh_kind(), fresh_kind()
                unify_kind (range_of_texpr a') (tt top_env' a) (KindFun(q,w))
                let x = fresh_var' q
                f x b
                unify r s (TyApply(a,x,w))
        | RawTTerm(r,a) -> assert_bound_vars env a; unify r s (TySymbol "<term>")
        | RawTMacro(r,a) ->
            List.map (function
                | RawMacroText(_,a) -> TMText a
                | RawMacroTypeVar(r,a) ->
                    match v_ty env a with
                    | Some a -> hover_types.Add(r,a); TMVar a
                    | None -> errors.Add(r,UnboundVariable); TMText "<error>"
                | RawMacroTermVar _ -> failwith "Compiler error: Term vars should never appear at the type level."
                ) a
            |> TyMacro |> unify r s
        | RawTMetaVar _ -> failwith "Compiler error: This particular metavar is only for typecase's clauses. This happens during the bottom-up segment."
    and pattern env s a = 
        let is_first = System.Collections.Generic.HashSet()
        let ho_make (i : int) (l : Var list) =
            let h = TyHigherOrder(i,List.foldBack (fun (x : Var) s -> KindFun(x.kind,s)) l KindType)
            let l' = List.map (fun (x : Var) -> x, fresh_subst_var x.constraints x.kind) l
            List.fold (fun s (_,x) -> match tt top_env' s with KindFun(_,k) -> TyApply(s,x,k) | _ -> failwith "impossible") h l', l'
        let rec ho_index = function 
            | TyApply(a,_,_) -> ho_index a 
            | TyHigherOrder(i,_) -> ValueSome i
            | _ -> ValueNone
        let rec ho_fun = function
            | TyFun(_,a) | TyForall(_,a) -> ho_fun a
            | a -> ho_index a
        let rec loop (env : Env) s a =
            let f = loop env
            match a with
            | PatB r -> unify r s TyB; env
            | PatE r -> hover_types.Add(r,s); env
            | PatVar(r,a) ->
                hover_types.Add(r,s)
                if is_first.Add a then {env with term=Map.add a s env.term}
                else unify r s env.term.[a]; env
            | PatDyn(_,a) -> f s a
            | PatAnnot(_,a,b) -> ty env s b; f s a
            | PatWhen(_,a,b) -> let env = f s a in term env (TyPrim BoolT) b; env
            | PatPair(r,a,b) ->
                let q,w = fresh_var(), fresh_var()
                unify r s (TyPair(q,w))
                pattern (pattern env q a) w b
            | PatSymbol(r,a) -> unify r s (TySymbol a); env
            | PatActive(r,a,b) ->
                let w,z = fresh_var(),fresh_var()
                unify r z (TyFun(s, w))
                term env z a
                f w b
            | PatOr(_,a,b) | PatAnd(_,a,b) -> pattern (pattern env s a) s b
            | PatValue(r,a) -> unify r s (lit a); env
            | PatDefaultValue(r,_) -> hover_types.Add(r,s); unify r s (fresh_subst_var (Set.singleton CNumber) KindType); env
            | PatRecordMembers(r,l) ->
                let l =
                    List.choose (function
                        | PatRecordMembersSymbol((r,a),b) -> Some (a,b)
                        | PatRecordMembersInjectVar((r,a),b) ->
                            match v_term env a |> Option.map visit_t with
                            | Some (TySymbol a as x) -> hover_types.Add(r,x); Some (a,b)
                            | Some x -> errors.Add(r, ExpectedSymbolAsRecordKey x); None
                            | None -> errors.Add(r, UnboundVariable); None
                        ) l
                match visit_t s with
                | TyRecord l' as s ->
                    let l, missing =
                        List.mapFoldBack (fun (a,b) missing ->
                            match Map.tryFind a l' with
                            | Some x -> (x,b), missing
                            | None -> (fresh_var(),b), a :: missing
                            ) l []
                    if List.isEmpty missing = false then errors.Add(r, MissingRecordFieldsInPattern(s, missing))
                    List.fold (fun env (a,b) -> pattern env a b) env l
                | s ->
                    let l, env =
                        List.mapFold (fun env (a,b) -> 
                            let v = fresh_var()
                            (a, v), pattern env v b
                            ) env l
                    unify r s (l |> Map |> TyRecord)
                    env
            | PatUnbox(r,PatPair(_,PatSymbol(r',name), a)) ->
                let assume i =
                    match hoc.[i] with
                    | HOCUnion(_,vars,cases) ->
                        hover_types.Add(r',s)
                        let x,m = ho_make i vars
                        unify r s x
                        match Map.tryFind name cases with
                        | Some v -> f (subst m v) a
                        | None -> errors.Add(r,CasePatternNotFoundForType i); f (fresh_var()) a
                    | HOCNominal _ -> errors.Add(r,NominalInPatternUnbox i); f (fresh_var()) a
                match term_subst s |> ho_index with
                | ValueSome i -> assume i
                | ValueNone ->
                    match v_term env name with
                    | Some x -> 
                        match term_subst x |> ho_fun with
                        | ValueSome i -> assume i
                        | ValueNone -> errors.Add(r,CannotInferCasePatternFromTermInEnv x); f (fresh_var()) a
                    | None -> errors.Add(r,CasePatternNotFound); f (fresh_var()) a
            | PatUnbox _ -> failwith "Compiler error: Malformed PatUnbox."
            | PatNominal(_,(r,name),a) ->
                match v_ty env name with
                | Some x -> 
                    match ho_index x with
                    | ValueSome i ->
                        match hoc.[i] with
                        | HOCNominal(_,vars,v) -> let x,m = ho_make i vars in unify r s x; f (subst m v) a
                        | HOCUnion _ -> errors.Add(r,UnionInPatternNominal i); f (fresh_var()) a
                    | ValueNone -> errors.Add(r,TypeInGlobalEnvIsNotNominal x); f (fresh_var()) a
                | _ -> errors.Add(r,UnboundVariable); f (fresh_var()) a
        loop env s a

    match expr with
    | BundleType(_,(r,name),vars,expr) ->
        let vars,env_ty = hovars vars
        let v = fresh_var()
        ty {term=Map.empty; ty=env_ty} v expr
        let t = List.foldBack (fun x s -> TyInl(x,s)) vars (term_subst v) // Note: Using visit_t instead of term_subst results in concurrency bugs.
        hover_types.Add(r,t)
        {top_env' with ty = Map.add name t top_env.ty}
    | BundleRecType l ->
        let l,_ =
            List.mapFold (fun i -> function
                | BundleNominal(_,name,vars,l) -> Choice1Of2(i,name,hovars vars,l), i+1
                | BundleUnion(_,name,vars,l) -> Choice2Of2(i,name,hovars vars,l), i+1
                ) hoc.Length l
        let env_ty = 
            List.fold (fun s (Choice1Of2(i,(_,name),(vars,_),_) | Choice2Of2(i,(_,name),(vars,_),_)) ->
                let tt = List.foldBack (fun (x : Var) s -> KindFun(x.kind,s)) vars KindType
                Map.add name (TyHigherOrder(i,tt)) s
                ) top_env.ty l
        let hoc,env_term =
            List.fold (fun (hoc, term) x ->
                let wrap_forall vars body t =
                    let tt = match t with TyHigherOrder(_,tt) -> tt | _ -> failwith "impossible"
                    let t,_ =
                        List.fold (fun (t,tt) x ->
                            let tt = match tt with KindFun(_,tt) -> tt | _ -> failwith "impossible"
                            TyApply(t,TyVar x,tt), tt
                            ) (t,tt) vars
                    List.foldBack (fun var ty -> TyForall(var,ty)) vars (TyFun(body,t))

                match x with
                | Choice1Of2(_,(r,name),(vars,env_ty'),expr) ->
                    let v = fresh_var()
                    ty {term=Map.empty; ty=Map.foldBack Map.add env_ty' env_ty} v expr 
                    let v = term_subst v
                    let inl_v = wrap_forall vars v env_ty.[name]
                    hover_types.Add(r,inl_v)
                    PersistentVector.conj (HOCNominal(name,vars,v)) hoc, Map.add name inl_v term
                | Choice2Of2(_,(_,name),(vars,env_ty'),l) ->
                    List.fold (fun (cases,term) expr ->
                        let v = fresh_var()
                        ty {term=Map.empty; ty=Map.foldBack Map.add env_ty' env_ty} v expr 
                        match term_subst v with
                        | TyPair(TySymbol x, b) -> 
                            if Map.containsKey x cases then errors.Add(range_of_texpr expr, DuplicateKeyInUnion); cases, term
                            else Map.add x b cases, Map.add x (wrap_forall vars b env_ty.[name]) term
                        | x -> errors.Add (range_of_texpr expr, ExpectedSymbolAsUnionKey); cases, term
                        ) (Map.empty, term) l
                    |> fun (l, term) -> PersistentVector.conj (HOCUnion(name,vars,l)) hoc, term
                ) (hoc, top_env.term) l
        {top_env' with hoc = hoc; ty = env_ty; term = env_term}
    | BundleInl(_,name,a,true) -> 
        let env = inl {term=Map.empty; ty=Map.empty} (name,a)
        {top_env' with term = Map.foldBack Map.add env.term top_env.term}
    | BundleInl(_,(_,name),a,false) ->
        assert_bound_vars {term=Map.empty; ty=Map.empty} a
        {top_env' with term = Map.add name (TySymbol "<real>") top_env.term }
    | BundleRecTerm l ->
        let _ =
            let h = HashSet()
            List.iter (fun (BundleRecInl(_,(r,n),_,_)) -> if h.Add n = false then errors.Add(r,DuplicateRecInlName)) l
        match l with 
        | BundleRecInl(_,_,_,true) :: _ -> 
            let l = List.map (function BundleRecInl(_,a,b,_) -> a,b) l
            (rec_block {term=Map.empty; ty=Map.empty} l).term
        | _ ->
            let env_term = List.fold (fun s (BundleRecInl(_,(_,a),_,_)) -> Map.add a (TySymbol "<real>") s) Map.empty l
            l |> List.iter (fun (BundleRecInl(_,_,x,_)) -> assert_bound_vars {term = env_term; ty = Map.empty} x)
            env_term
        |> fun env_term -> {top_env' with term = Map.foldBack Map.add env_term top_env.term}
    | BundlePrototype(_,(r,name),(_,var_init),vars,expr) ->
        let cons = CPrototype top_env'.prototypes.Length
        let v = {scope=0; constraints=Set.singleton cons; name=var_init; kind=List.foldBack (fun ((_,(_,k)),_) s -> KindFun(typevar k, s)) vars KindType}
        let vars,env_ty = typevars (Map.add var_init (TyVar v) Map.empty) vars
        let vars = v :: vars
        let v = fresh_var()
        ty {term=Map.empty; ty=env_ty} v expr
        let body = List.foldBack (fun a b -> TyForall(a,b)) vars (term_subst v)
        hover_types.Add(r,body)
        { top_env' with term = Map.add name body top_env.term; ty = Map.add name (TyConstraint cons) top_env.ty; 
                        prototypes = PersistentVector.conj {|instances=Map.empty; name=name; signature=body|} top_env'.prototypes }
    | BundleInstance(r,prot,ins,vars,body) ->
        let assert_no_kind x = x |> List.iter (fun ((r,(_,k)),_) -> match k with RawKindWildcard -> () | _ -> errors.Add(r,KindNotAllowedInInstanceForall))
        let assert_vars_count vars_count vars_expected = if vars_count <> vars_expected then errors.Add(r,InstanceCoreVarsShouldMatchTheArityDifference(vars_count,vars_expected))
        let assert_kind_compatibility got expected =
            try unify_kind' (fun () -> raise (TypeErrorException [r, InstanceKindError (got, expected)])) got expected
            with :? TypeErrorException as e -> errors.AddRange e.Data0
        let assert_kind_arity prot_kind_arity ins_kind_arity = if ins_kind_arity < prot_kind_arity then errors.Add(r,InstanceArityError(prot_kind_arity,ins_kind_arity))
        let assert_instance_forall_does_not_shadow_prototype_forall prot_forall_name = List.iter (fun ((r,(a,_)),_) -> if a = prot_forall_name then errors.Add(r,InstanceVarShouldNotMatchAnyOfPrototypes)) vars
        let body prot_id (ins_id,ins_kind') = 
            let er_count = errors.Count
            let guard next = if errors.Count = er_count then next () else top_env'
            let ins_kind = kind_get ins_kind'
            let prototype = top_env'.prototypes.[prot_id]
            hover_types.Add(fst prot, prototype.signature) // TODO: Do the same for the instance signature.
            let prototype_init_forall_kind = prototype_init_forall_kind prototype.signature
            let prot_kind = kind_get prototype_init_forall_kind
            assert_kind_arity prot_kind.arity ins_kind.arity
            guard <| fun () ->
            let vars_expected = ins_kind.arity - prot_kind.arity
            assert_kind_compatibility (List.skip vars_expected ins_kind.args |> List.reduceBack (fun a b -> KindFun(a,b))) prototype_init_forall_kind
            guard <| fun () ->
            assert_vars_count (List.length vars) vars_expected
            guard <| fun () ->
            assert_no_kind vars
            guard <| fun () ->
            let ins_core, env_ty, ins_constraints =
                let ins_vars, env_ty =
                    List.mapFold (fun s (((r,_),_) & x,k) ->
                        let v = {typevar_to_var s x with kind = k}
                        let x = TyVar v
                        hover_types.Add(r,x)
                        x, Map.add v.name x s
                        ) Map.empty (List.zip vars (List.take vars_expected ins_kind.args))
                let ins_constraints = ins_vars |> List.map (function TyVar x -> x.constraints | _ -> failwith "impossible")
                let ins_core, _ =
                    let trim_kind = function KindFun(_,k) -> k | _ -> failwith "impossible"
                    List.fold (fun (a,k) (b : T) -> let k = trim_kind k in TyApply(a,b,k),k) (TyHigherOrder(ins_id,ins_kind'),ins_kind') ins_vars
                ins_core, env_ty, ins_constraints
            let env_ty, prot_body =
                match foralls_ty_get prototype.signature with
                | (prot_core :: prot_foralls), prot_body ->
                    List.fold (fun ty x ->
                        assert_instance_forall_does_not_shadow_prototype_forall x.name
                        Map.add x.name (TyVar x) ty) env_ty prot_foralls,
                    subst [prot_core, ins_core] prot_body
                | _ -> failwith "impossible"
            term {term=Map.empty; ty=env_ty} prot_body body
            let prototype = {|prototype with instances = Map.add ins_id ins_constraints prototype.instances|}
            {top_env' with prototypes = PersistentVector.update prot_id prototype top_env'.prototypes}
            
        let fake _ = top_env'
        let check_ins on_succ =
            match Map.tryFind (snd ins) top_env.ty with
            | None -> errors.Add(fst ins, UnboundVariable); top_env'
            | Some(TyHigherOrder(i',k)) -> on_succ (i',k)
            | Some x -> errors.Add(fst ins, ExpectedHigherOrder x); top_env'
        match Map.tryFind (snd prot) top_env.ty with
        | None -> errors.Add(fst prot, UnboundVariable); check_ins fake
        | Some(TyConstraint(CPrototype i)) -> check_ins (body i)
        | Some x -> errors.Add(fst prot, ExpectedPrototype x); check_ins fake
    |> fun top_env' -> 
        type_application |> Seq.iter (fun x -> if has_metavars x.Value then errors.Add(range_of_expr x.Key, ValueRestriction x.Value))
        let hovers = hover_types |> Seq.toArray |> Array.map (fun x -> x.Key, show_t top_env' x.Value)
        let errors = errors |> Seq.toList |> List.map (fun (a,b) -> show_type_error top_env' b, a)
        (hovers, errors), top_env'

let default_env : TopEnv = 
    let inline inl f = let v = {scope=0; kind=KindType; constraints=Set.empty; name="x"} in TyInl(v,f v)
         
    let ty = 
        [
        "number", TyConstraint CNumber
        "i8", TyPrim Int8T
        "i16", TyPrim Int16T
        "i32", TyPrim Int32T
        "i64", TyPrim Int64T
        "u8", TyPrim UInt8T
        "u16", TyPrim UInt16T
        "u32", TyPrim UInt32T
        "u64", TyPrim UInt64T
        "f32", TyPrim Float32T
        "f64", TyPrim Float64T
        "string", TyPrim StringT
        "bool", TyPrim BoolT
        "char", TyPrim CharT
        "array", inl (fun x -> TyArray(TyVar x))
        "heap", inl (fun x -> TyLayout(TyVar x,Layout.Heap))
        "mut", inl (fun x -> TyLayout(TyVar x,Layout.HeapMutable))
        ] |> Map.ofList
    {hoc=PersistentVector.empty; ty=ty; term=Map.empty; prototypes=PersistentVector.empty}