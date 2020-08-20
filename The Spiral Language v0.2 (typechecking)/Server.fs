﻿module Spiral.Server

open System
open System.Collections.Generic
open FSharp.Json
open NetMQ
open NetMQ.Sockets
open Spiral.Config
open Spiral.Tokenize
open Spiral.Blockize

type ClientReq =
    | ProjectFileOpen of {|uri : string; spiprojText : string|}
    | FileOpen of {|uri : string; spiText : string|}
    | FileChanged of {|uri : string; spiEdit : SpiEdit|}
    | FileTokenRange of {|uri : string; range : VSCRange|}

type ProjectFileRes = VSCErrorOpt []
type FileOpenRes = VSCError []
type FileChangeRes = VSCError []
type FileTokenAllRes = VSCTokenArray
type FileTokenChangesRes = int * int * VSCTokenArray

let port = 13805
let uri_parser_errors = sprintf "tcp://localhost:%i/parser_errors" port
let uri = sprintf "tcp://*:%i" port

open Hopac
open Hopac.Infixes
open Hopac.Extensions
let server () =
    let server_blockizer = Utils.memoize (Dictionary()) (fun (x : string) -> run (Blockize.server (IO.Path.GetExtension(x) = ".spi")))
    use sock = new ResponseSocket()
    sock.Options.ReceiveHighWatermark <- Int32.MaxValue
    sock.Bind(uri)
    printfn "Server bound to: %s" uri

    while true do
        // TODO: The message id here is for debugging purposes. I'll remove it at some point.
        let (id : int), x = Json.deserialize(sock.ReceiveFrameString())
        match x with
        | ProjectFileOpen x ->
            match config x.uri x.spiprojText with Ok x -> [||] | Error x -> x
            |> Json.serialize
        | FileOpen x ->
            (let res = IVar() in Ch.give (server_blockizer x.uri) (Req.Put(x.spiText,res)) >>=. IVar.read res)
            |> run |> Json.serialize
        | FileChanged x ->
            (let res = IVar() in Ch.give (server_blockizer x.uri) (Req.Modify(x.spiEdit,res)) >>=. IVar.read res)
            |> run |> Json.serialize
        | FileTokenRange x ->
            (let res = IVar() in Ch.give (server_blockizer x.uri) (Req.GetRange(x.range,res)) >>=. IVar.read res)
            |> run |> Json.serialize
        |> sock.SendFrame

server()

