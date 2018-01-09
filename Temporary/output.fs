module SpiralExample.Main
let cuda_kernels = """
#include "cub/cub.cuh"

extern "C" {
    typedef float(*FunPointer0)(float, float);
    __global__ void method_7(float * var_0, float * var_1);
    __global__ void method_9(float * var_0, float * var_1, float * var_2);
    __global__ void method_17(float var_0, float var_1, float * var_2, float * var_3, float * var_4, float * var_5);
    __global__ void method_19(float * var_0, float * var_1, float * var_2, float * var_3, float * var_4);
    __device__ void method_8(float * var_0, float * var_1, long long int var_2);
    __device__ float method_10(float * var_0, float * var_1, float var_2, long long int var_3);
    __device__ float method_11(float var_0, float var_1);
    __device__ void method_18(float var_0, float var_1, float * var_2, float * var_3, float * var_4, float * var_5, long long int var_6);
    __device__ void method_20(float * var_0, float * var_1, float * var_2, float * var_3, float * var_4, long long int var_5);
    
    __global__ void method_7(float * var_0, float * var_1) {
        long long int var_2 = threadIdx.x;
        long long int var_3 = threadIdx.y;
        long long int var_4 = threadIdx.z;
        long long int var_5 = blockIdx.x;
        long long int var_6 = blockIdx.y;
        long long int var_7 = blockIdx.z;
        long long int var_8 = (var_5 * 128);
        long long int var_9 = (var_8 + var_2);
        method_8(var_0, var_1, var_9);
    }
    __global__ void method_9(float * var_0, float * var_1, float * var_2) {
        long long int var_3 = threadIdx.x;
        long long int var_4 = threadIdx.y;
        long long int var_5 = threadIdx.z;
        long long int var_6 = blockIdx.x;
        long long int var_7 = blockIdx.y;
        long long int var_8 = blockIdx.z;
        long long int var_9 = (var_6 * 128);
        long long int var_10 = (var_9 + var_3);
        float var_11 = 0;
        float var_12 = method_10(var_0, var_1, var_11, var_10);
        FunPointer0 var_15 = method_11;
        float var_16 = cub::BlockReduce<float,128>().Reduce(var_12, var_15);
        char var_17 = (var_3 == 0);
        if (var_17) {
            char var_18 = (var_6 >= 0);
            char var_20;
            if (var_18) {
                var_20 = (var_6 < 1);
            } else {
                var_20 = 0;
            }
            char var_21 = (var_20 == 0);
            if (var_21) {
                // unprinted assert;
            } else {
            }
            var_2[var_6] = var_16;
        } else {
        }
    }
    __global__ void method_17(float var_0, float var_1, float * var_2, float * var_3, float * var_4, float * var_5) {
        long long int var_6 = threadIdx.x;
        long long int var_7 = threadIdx.y;
        long long int var_8 = threadIdx.z;
        long long int var_9 = blockIdx.x;
        long long int var_10 = blockIdx.y;
        long long int var_11 = blockIdx.z;
        long long int var_12 = (var_9 * 128);
        long long int var_13 = (var_12 + var_6);
        method_18(var_0, var_1, var_2, var_3, var_4, var_5, var_13);
    }
    __global__ void method_19(float * var_0, float * var_1, float * var_2, float * var_3, float * var_4) {
        long long int var_5 = threadIdx.x;
        long long int var_6 = threadIdx.y;
        long long int var_7 = threadIdx.z;
        long long int var_8 = blockIdx.x;
        long long int var_9 = blockIdx.y;
        long long int var_10 = blockIdx.z;
        long long int var_11 = (var_8 * 128);
        long long int var_12 = (var_11 + var_5);
        method_20(var_0, var_1, var_2, var_3, var_4, var_12);
    }
    __device__ void method_8(float * var_0, float * var_1, long long int var_2) {
        char var_3 = (var_2 < 8);
        if (var_3) {
            char var_4 = (var_2 >= 0);
            char var_5 = (var_4 == 0);
            if (var_5) {
                // unprinted assert;
            } else {
            }
            if (var_5) {
                // unprinted assert;
            } else {
            }
            float var_6 = var_0[var_2];
            float var_7 = (-var_6);
            float var_8 = exp(var_7);
            float var_9 = (1 + var_8);
            float var_10 = (1 / var_9);
            var_1[var_2] = var_10;
            long long int var_11 = (var_2 + 128);
            method_8(var_0, var_1, var_11);
        } else {
        }
    }
    __device__ float method_10(float * var_0, float * var_1, float var_2, long long int var_3) {
        char var_4 = (var_3 < 8);
        if (var_4) {
            char var_5 = (var_3 >= 0);
            char var_6 = (var_5 == 0);
            if (var_6) {
                // unprinted assert;
            } else {
            }
            float var_7 = var_0[var_3];
            float var_8 = var_1[var_3];
            float var_9 = (var_8 - var_7);
            float var_10 = (var_9 * var_9);
            float var_11 = (var_2 + var_10);
            long long int var_12 = (var_3 + 128);
            return method_10(var_0, var_1, var_11, var_12);
        } else {
            return var_2;
        }
    }
    __device__ float method_11(float var_0, float var_1) {
        return (var_0 + var_1);
    }
    __device__ void method_18(float var_0, float var_1, float * var_2, float * var_3, float * var_4, float * var_5, long long int var_6) {
        char var_7 = (var_6 < 8);
        if (var_7) {
            char var_8 = (var_6 >= 0);
            char var_9 = (var_8 == 0);
            if (var_9) {
                // unprinted assert;
            } else {
            }
            if (var_9) {
                // unprinted assert;
            } else {
            }
            float var_10 = var_2[var_6];
            float var_11 = var_3[var_6];
            float var_12 = var_4[var_6];
            float var_13 = (var_11 - var_12);
            float var_14 = (2 * var_13);
            float var_15 = (var_0 * var_14);
            float var_16 = (var_10 + var_15);
            var_5[var_6] = var_16;
            long long int var_17 = (var_6 + 128);
            method_18(var_0, var_1, var_2, var_3, var_4, var_5, var_17);
        } else {
        }
    }
    __device__ void method_20(float * var_0, float * var_1, float * var_2, float * var_3, float * var_4, long long int var_5) {
        char var_6 = (var_5 < 8);
        if (var_6) {
            char var_7 = (var_5 >= 0);
            char var_8 = (var_7 == 0);
            if (var_8) {
                // unprinted assert;
            } else {
            }
            if (var_8) {
                // unprinted assert;
            } else {
            }
            float var_9 = var_0[var_5];
            float var_10 = var_1[var_5];
            float var_11 = var_2[var_5];
            float var_12 = var_3[var_5];
            float var_13 = (1 - var_12);
            float var_14 = (var_12 * var_13);
            float var_15 = (var_11 * var_14);
            float var_16 = (var_9 + var_15);
            var_4[var_5] = var_16;
            long long int var_17 = (var_5 + 128);
            method_20(var_0, var_1, var_2, var_3, var_4, var_17);
        } else {
        }
    }
}
"""

type Union0 =
    | Union0Case0 of Tuple1
    | Union0Case1
and Tuple1 =
    struct
    val mem_0: ManagedCuda.BasicTypes.CUdeviceptr
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and Env2 =
    struct
    val mem_0: Env3
    val mem_1: int64
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
and Env3 =
    struct
    val mem_0: (Union0 ref)
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and Union4 =
    | Union4Case0 of Tuple5
    | Union4Case1
and Tuple5 =
    struct
    val mem_0: float32
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
let rec method_0 ((var_0: System.Diagnostics.DataReceivedEventArgs)): unit =
    let (var_1: string) = var_0.get_Data()
    let (var_2: string) = System.String.Format("{0}",var_1)
    System.Console.WriteLine(var_2)
and method_1((var_0: (Union0 ref))): ManagedCuda.BasicTypes.CUdeviceptr =
    let (var_1: Union0) = (!var_0)
    match var_1 with
    | Union0Case0(var_2) ->
        var_2.mem_0
    | Union0Case1 ->
        (failwith "A Cuda memory cell that has been disposed has been tried to be accessed.")
and method_2((var_0: uint64), (var_1: System.Collections.Generic.Stack<Env2>), (var_2: uint64), (var_3: int64)): Env3 =
    let (var_4: int32) = var_1.get_Count()
    let (var_5: bool) = (var_4 > 0)
    if var_5 then
        let (var_6: Env2) = var_1.Peek()
        let (var_7: Env3) = var_6.mem_0
        let (var_8: int64) = var_6.mem_1
        let (var_9: (Union0 ref)) = var_7.mem_0
        let (var_10: Union0) = (!var_9)
        match var_10 with
        | Union0Case0(var_11) ->
            let (var_12: ManagedCuda.BasicTypes.CUdeviceptr) = var_11.mem_0
            method_3((var_12: ManagedCuda.BasicTypes.CUdeviceptr), (var_0: uint64), (var_2: uint64), (var_3: int64), (var_1: System.Collections.Generic.Stack<Env2>), (var_7: Env3), (var_8: int64))
        | Union0Case1 ->
            let (var_14: Env2) = var_1.Pop()
            let (var_15: Env3) = var_14.mem_0
            let (var_16: int64) = var_14.mem_1
            method_2((var_0: uint64), (var_1: System.Collections.Generic.Stack<Env2>), (var_2: uint64), (var_3: int64))
    else
        method_4((var_0: uint64), (var_2: uint64), (var_3: int64), (var_1: System.Collections.Generic.Stack<Env2>))
and method_5((var_0: (Union0 ref))): ManagedCuda.BasicTypes.CUdeviceptr =
    let (var_1: Union0) = (!var_0)
    match var_1 with
    | Union0Case0(var_2) ->
        var_2.mem_0
    | Union0Case1 ->
        (failwith "A Cuda memory cell that has been disposed has been tried to be accessed.")
and method_6((var_0: ManagedCuda.CudaBlas.CudaBlasHandle), (var_1: int32), (var_2: int32), (var_3: int32), (var_4: (float32 ref)), (var_5: ManagedCuda.BasicTypes.CUdeviceptr), (var_6: int32), (var_7: ManagedCuda.BasicTypes.CUdeviceptr), (var_8: int32), (var_9: (float32 ref)), (var_10: ManagedCuda.BasicTypes.CUdeviceptr), (var_11: int32)): unit =
    let (var_12: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_13: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_14: ManagedCuda.CudaBlas.CublasStatus) = ManagedCuda.CudaBlas.CudaBlasNativeMethods.cublasSgemm_v2(var_0, var_12, var_13, var_1, var_2, var_3, var_4, var_5, var_6, var_7, var_8, var_9, var_10, var_11)
    if var_14 <> ManagedCuda.CudaBlas.CublasStatus.Success then raise <| new ManagedCuda.CudaBlas.CudaBlasException(var_14)
and method_16((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_2: (Union4 ref)), (var_3: (Union4 ref))): float32 =
    let (var_4: Union4) = (!var_3)
    match var_4 with
    | Union4Case0(var_5) ->
        var_5.mem_0
    | Union4Case1 ->
        let (var_7: float32) = method_14((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_2: (Union4 ref)))
        var_3 := (Union4Case0(Tuple5(var_7)))
        var_7
and method_15((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_2: (Union4 ref))): float32 =
    let (var_3: Union4) = (!var_2)
    match var_3 with
    | Union4Case0(var_4) ->
        var_4.mem_0
    | Union4Case1 ->
        let (var_6: float32) = method_12((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext))
        var_2 := (Union4Case0(Tuple5(var_6)))
        var_6
and method_21((var_0: ManagedCuda.CudaBlas.CudaBlasHandle), (var_1: int32), (var_2: int32), (var_3: int32), (var_4: (float32 ref)), (var_5: ManagedCuda.BasicTypes.CUdeviceptr), (var_6: int32), (var_7: ManagedCuda.BasicTypes.CUdeviceptr), (var_8: int32), (var_9: (float32 ref)), (var_10: ManagedCuda.BasicTypes.CUdeviceptr), (var_11: int32)): unit =
    let (var_12: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.Transpose
    let (var_13: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_14: ManagedCuda.CudaBlas.CublasStatus) = ManagedCuda.CudaBlas.CudaBlasNativeMethods.cublasSgemm_v2(var_0, var_12, var_13, var_1, var_2, var_3, var_4, var_5, var_6, var_7, var_8, var_9, var_10, var_11)
    if var_14 <> ManagedCuda.CudaBlas.CublasStatus.Success then raise <| new ManagedCuda.CudaBlas.CudaBlasException(var_14)
and method_3((var_0: ManagedCuda.BasicTypes.CUdeviceptr), (var_1: uint64), (var_2: uint64), (var_3: int64), (var_4: System.Collections.Generic.Stack<Env2>), (var_5: Env3), (var_6: int64)): Env3 =
    let (var_7: ManagedCuda.BasicTypes.SizeT) = var_0.Pointer
    let (var_8: uint64) = uint64 var_7
    let (var_9: uint64) = uint64 var_6
    let (var_10: uint64) = (var_8 - var_1)
    let (var_11: uint64) = (var_10 + var_9)
    let (var_12: uint64) = uint64 var_3
    let (var_13: uint64) = (var_12 + var_11)
    let (var_14: bool) = (var_13 <= var_2)
    let (var_15: bool) = (var_14 = false)
    if var_15 then
        (failwith "Cache size has been exceeded in the allocator.")
    else
        ()
    let (var_16: uint64) = (var_8 + var_9)
    let (var_17: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_16)
    let (var_18: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_17)
    let (var_19: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_18))))
    var_4.Push((Env2((Env3(var_19)), var_3)))
    (Env3(var_19))
and method_4((var_0: uint64), (var_1: uint64), (var_2: int64), (var_3: System.Collections.Generic.Stack<Env2>)): Env3 =
    let (var_4: uint64) = uint64 var_2
    let (var_5: bool) = (var_4 <= var_1)
    let (var_6: bool) = (var_5 = false)
    if var_6 then
        (failwith "Cache size has been exceeded in the allocator.")
    else
        ()
    let (var_7: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_0)
    let (var_8: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_7)
    let (var_9: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_8))))
    var_3.Push((Env2((Env3(var_9)), var_2)))
    (Env3(var_9))
and method_14((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_2: (Union4 ref))): float32 =
    let (var_3: float32) = method_15((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_2: (Union4 ref)))
    (var_3 / 2.000000f)
and method_12((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext)): float32 =
    let (var_2: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_0: (Union0 ref)))
    let (var_3: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(1L))
    var_1.CopyToHost(var_3, var_2)
    var_1.Synchronize()
    let (var_4: float32) = 0.000000f
    let (var_5: int64) = 0L
    method_13((var_3: (float32 [])), (var_4: float32), (var_5: int64))
and method_13((var_0: (float32 [])), (var_1: float32), (var_2: int64)): float32 =
    let (var_3: bool) = (var_2 < 1L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: float32) = var_0.[int32 var_2]
        let (var_7: float32) = (var_1 + var_6)
        let (var_8: int64) = (var_2 + 1L)
        method_13((var_0: (float32 [])), (var_7: float32), (var_8: int64))
    else
        var_1
let (var_0: string) = cuda_kernels
let (var_1: ManagedCuda.CudaContext) = ManagedCuda.CudaContext(false)
var_1.Synchronize()
let (var_2: string) = System.Environment.get_CurrentDirectory()
let (var_3: string) = System.IO.Path.Combine(var_2, "nvcc_router.bat")
let (var_4: System.Diagnostics.ProcessStartInfo) = System.Diagnostics.ProcessStartInfo()
var_4.set_RedirectStandardOutput(true)
var_4.set_RedirectStandardError(true)
var_4.set_UseShellExecute(false)
var_4.set_FileName(var_3)
let (var_5: System.Diagnostics.Process) = System.Diagnostics.Process()
var_5.set_StartInfo(var_4)
let (var_7: (System.Diagnostics.DataReceivedEventArgs -> unit)) = method_0
var_5.OutputDataReceived.Add(var_7)
var_5.ErrorDataReceived.Add(var_7)
let (var_8: string) = System.IO.Path.Combine("C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community", "VC\\Auxiliary\\Build\\vcvars64.bat")
let (var_9: string) = System.IO.Path.Combine("C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community", "VC\\Tools\\MSVC\\14.11.25503\\bin\\Hostx64\\x64")
let (var_10: string) = System.IO.Path.Combine("C:\\Program Files\\NVIDIA GPU Computing Toolkit\\CUDA\\v9.0", "include")
let (var_11: string) = System.IO.Path.Combine("C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community", "VC\\Tools\\MSVC\\14.11.25503\\include")
let (var_12: string) = System.IO.Path.Combine("C:\\Program Files\\NVIDIA GPU Computing Toolkit\\CUDA\\v9.0", "bin\\nvcc.exe")
let (var_13: string) = System.IO.Path.Combine(var_2, "cuda_kernels.ptx")
let (var_14: string) = System.IO.Path.Combine(var_2, "cuda_kernels.cu")
let (var_15: bool) = System.IO.File.Exists(var_14)
if var_15 then
    System.IO.File.Delete(var_14)
else
    ()
System.IO.File.WriteAllText(var_14, var_0)
let (var_16: bool) = System.IO.File.Exists(var_3)
if var_16 then
    System.IO.File.Delete(var_3)
else
    ()
let (var_17: System.IO.FileStream) = System.IO.File.OpenWrite(var_3)
let (var_18: System.IO.StreamWriter) = System.IO.StreamWriter(var_17)
var_18.WriteLine("SETLOCAL")
let (var_19: string) = String.concat "" [|"CALL "; "\""; var_8; "\""|]
var_18.WriteLine(var_19)
let (var_20: string) = String.concat "" [|"SET PATH=%PATH%;"; "\""; var_9; "\""|]
var_18.WriteLine(var_20)
let (var_21: string) = String.concat "" [|"\""; var_12; "\" -gencode=arch=compute_30,code=\\\"sm_30,compute_30\\\" --use-local-env --cl-version 2017 -I\""; var_10; "\" -I\"C:\\cub-1.7.4\" -I\""; var_11; "\" --keep-dir \""; var_2; "\" -maxrregcount=0  --machine 64 -ptx -cudart static  -o \""; var_13; "\" \""; var_14; "\""|]
var_18.WriteLine(var_21)
var_18.Dispose()
var_17.Dispose()
let (var_22: System.Diagnostics.Stopwatch) = System.Diagnostics.Stopwatch.StartNew()
let (var_23: bool) = var_5.Start()
let (var_24: bool) = (var_23 = false)
if var_24 then
    (failwith "NVCC failed to run.")
else
    ()
var_5.BeginOutputReadLine()
var_5.BeginErrorReadLine()
var_5.WaitForExit()
let (var_25: int32) = var_5.get_ExitCode()
let (var_26: bool) = (var_25 = 0)
let (var_27: bool) = (var_26 = false)
if var_27 then
    let (var_28: string) = System.String.Format("{0}",var_25)
    let (var_29: string) = String.concat ", " [|"NVCC failed compilation."; var_28|]
    let (var_30: string) = System.String.Format("[{0}]",var_29)
    (failwith var_30)
else
    ()
let (var_31: System.TimeSpan) = var_22.get_Elapsed()
printfn "The time it took to compile the Cuda kernels is: %A" var_31
let (var_32: ManagedCuda.BasicTypes.CUmodule) = var_1.LoadModulePTX(var_13)
var_5.Dispose()
let (var_33: string) = String.concat "" [|"Compiled the kernels into the following directory: "; var_2|]
let (var_34: string) = System.String.Format("{0}",var_33)
System.Console.WriteLine(var_34)
let (var_35: ManagedCuda.CudaDeviceProperties) = var_1.GetDeviceInfo()
let (var_36: ManagedCuda.BasicTypes.SizeT) = var_35.get_TotalGlobalMemory()
let (var_37: int64) = int64 var_36
let (var_38: float) = float var_37
let (var_39: float) = (0.700000 * var_38)
let (var_40: int64) = int64 var_39
let (var_41: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_40)
let (var_42: ManagedCuda.BasicTypes.CUdeviceptr) = var_1.AllocateMemory(var_41)
let (var_43: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_42))))
let (var_44: System.Collections.Generic.Stack<Env2>) = System.Collections.Generic.Stack<Env2>()
let (var_45: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_43: (Union0 ref)))
let (var_46: ManagedCuda.BasicTypes.SizeT) = var_45.Pointer
let (var_47: uint64) = uint64 var_46
let (var_48: uint64) = uint64 var_40
let (var_49: ManagedCuda.CudaStream) = ManagedCuda.CudaStream()
let (var_50: ManagedCuda.CudaRand.GeneratorType) = ManagedCuda.CudaRand.GeneratorType.PseudoDefault
let (var_51: ManagedCuda.CudaRand.CudaRandDevice) = ManagedCuda.CudaRand.CudaRandDevice(var_50)
let (var_52: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_51.SetStream(var_52)
let (var_53: ManagedCuda.CudaBlas.PointerMode) = ManagedCuda.CudaBlas.PointerMode.Host
let (var_54: ManagedCuda.CudaBlas.AtomicsMode) = ManagedCuda.CudaBlas.AtomicsMode.Allowed
let (var_55: ManagedCuda.CudaBlas.CudaBlas) = ManagedCuda.CudaBlas.CudaBlas(var_53, var_54)
let (var_56: ManagedCuda.CudaBlas.CudaBlasHandle) = var_55.get_CublasHandle()
let (var_57: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_55.set_Stream(var_57)
let (var_58: int64) = 48L
let (var_59: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_58: int64))
let (var_60: (Union0 ref)) = var_59.mem_0
let (var_61: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_60: (Union0 ref)))
let (var_62: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(12L)
var_51.GenerateNormal32(var_61, var_62, 0.000000f, 1.000000f)
let (var_63: int64) = 32L
let (var_64: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_63: int64))
let (var_65: (Union0 ref)) = var_64.mem_0
let (var_66: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_65: (Union0 ref)))
let (var_67: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_68: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(32L)
var_1.ClearMemoryAsync(var_66, 0uy, var_68, var_67)
let (var_69: int64) = 96L
let (var_70: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_69: int64))
let (var_71: (Union0 ref)) = var_70.mem_0
let (var_72: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_71: (Union0 ref)))
let (var_73: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(24L)
var_51.GenerateNormal32(var_72, var_73, 0.000000f, 0.447214f)
let (var_74: int64) = 96L
let (var_75: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_74: int64))
let (var_76: (Union0 ref)) = var_75.mem_0
let (var_77: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_76: (Union0 ref)))
let (var_78: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_79: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(96L)
var_1.ClearMemoryAsync(var_77, 0uy, var_79, var_78)
let (var_80: int64) = 32L
let (var_81: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_80: int64))
let (var_82: (Union0 ref)) = var_81.mem_0
let (var_83: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_60: (Union0 ref)))
let (var_84: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_71: (Union0 ref)))
let (var_85: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_82: (Union0 ref)))
let (var_86: (float32 ref)) = (ref 1.000000f)
let (var_87: (float32 ref)) = (ref 0.000000f)
let (var_88: int32) = 2
let (var_89: int32) = 4
let (var_90: int32) = 6
let (var_91: int32) = 2
let (var_92: int32) = 6
let (var_93: int32) = 2
method_6((var_56: ManagedCuda.CudaBlas.CudaBlasHandle), (var_88: int32), (var_89: int32), (var_90: int32), (var_86: (float32 ref)), (var_83: ManagedCuda.BasicTypes.CUdeviceptr), (var_91: int32), (var_84: ManagedCuda.BasicTypes.CUdeviceptr), (var_92: int32), (var_87: (float32 ref)), (var_85: ManagedCuda.BasicTypes.CUdeviceptr), (var_93: int32))
let (var_94: int64) = 32L
let (var_95: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_94: int64))
let (var_96: (Union0 ref)) = var_95.mem_0
let (var_97: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_96: (Union0 ref)))
let (var_98: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_99: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(32L)
var_1.ClearMemoryAsync(var_97, 0uy, var_99, var_98)
let (var_104: int64) = 32L
let (var_105: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_104: int64))
let (var_106: (Union0 ref)) = var_105.mem_0
let (var_107: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_82: (Union0 ref)))
let (var_108: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_106: (Union0 ref)))
// Cuda join point
// method_7((var_107: ManagedCuda.BasicTypes.CUdeviceptr), (var_108: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_110: (System.Object [])) = [|var_107; var_108|]: (System.Object [])
let (var_111: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_7", var_32, var_1)
let (var_112: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
var_111.set_GridDimensions(var_112)
let (var_113: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_111.set_BlockDimensions(var_113)
let (var_114: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_111.RunAsync(var_114, var_110)
let (var_115: int64) = 32L
let (var_116: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_115: int64))
let (var_117: (Union0 ref)) = var_116.mem_0
let (var_118: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_117: (Union0 ref)))
let (var_119: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_120: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(32L)
var_1.ClearMemoryAsync(var_118, 0uy, var_120, var_119)
let (var_121: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_106: (Union0 ref)))
let (var_122: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_65: (Union0 ref)))
let (var_125: int64) = 4L
let (var_126: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_125: int64))
let (var_127: (Union0 ref)) = var_126.mem_0
let (var_128: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_127: (Union0 ref)))
// Cuda join point
// method_9((var_121: ManagedCuda.BasicTypes.CUdeviceptr), (var_122: ManagedCuda.BasicTypes.CUdeviceptr), (var_128: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_130: (System.Object [])) = [|var_121; var_122; var_128|]: (System.Object [])
let (var_131: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_9", var_32, var_1)
let (var_132: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
var_131.set_GridDimensions(var_132)
let (var_133: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_131.set_BlockDimensions(var_133)
let (var_134: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_131.RunAsync(var_134, var_130)
let (var_136: (Union4 ref)) = (ref Union4Case1)
let (var_137: (float32 ref)) = (ref 0.000000f)
let (var_139: (Union4 ref)) = (ref Union4Case1)
let (var_140: (float32 ref)) = (ref 0.000000f)
let (var_141: float32) = method_16((var_127: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_136: (Union4 ref)), (var_139: (Union4 ref)))
let (var_142: string) = System.String.Format("{0}",var_141)
let (var_143: string) = String.concat ", " [|"Cost is:"; var_142|]
let (var_144: string) = System.String.Format("[{0}]",var_143)
System.Console.WriteLine(var_144)
var_140 := 1.000000f
let (var_145: float32) = method_16((var_127: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_136: (Union4 ref)), (var_139: (Union4 ref)))
let (var_146: float32) = (!var_140)
let (var_147: float32) = method_15((var_127: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_136: (Union4 ref)))
let (var_148: float32) = (var_146 / 2.000000f)
let (var_149: float32) = (!var_137)
let (var_150: float32) = (var_149 + var_148)
var_137 := var_150
let (var_151: float32) = method_15((var_127: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_136: (Union4 ref)))
let (var_152: float32) = (!var_137)
let (var_153: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_117: (Union0 ref)))
let (var_154: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_106: (Union0 ref)))
let (var_155: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_65: (Union0 ref)))
let (var_156: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_117: (Union0 ref)))
// Cuda join point
// method_17((var_152: float32), (var_151: float32), (var_153: ManagedCuda.BasicTypes.CUdeviceptr), (var_154: ManagedCuda.BasicTypes.CUdeviceptr), (var_155: ManagedCuda.BasicTypes.CUdeviceptr), (var_156: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_158: (System.Object [])) = [|var_152; var_151; var_153; var_154; var_155; var_156|]: (System.Object [])
let (var_159: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_17", var_32, var_1)
let (var_160: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
var_159.set_GridDimensions(var_160)
let (var_161: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_159.set_BlockDimensions(var_161)
let (var_162: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_159.RunAsync(var_162, var_158)
let (var_163: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_96: (Union0 ref)))
let (var_164: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_82: (Union0 ref)))
let (var_165: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_117: (Union0 ref)))
let (var_166: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_106: (Union0 ref)))
let (var_167: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_96: (Union0 ref)))
// Cuda join point
// method_19((var_163: ManagedCuda.BasicTypes.CUdeviceptr), (var_164: ManagedCuda.BasicTypes.CUdeviceptr), (var_165: ManagedCuda.BasicTypes.CUdeviceptr), (var_166: ManagedCuda.BasicTypes.CUdeviceptr), (var_167: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_169: (System.Object [])) = [|var_163; var_164; var_165; var_166; var_167|]: (System.Object [])
let (var_170: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_19", var_32, var_1)
let (var_171: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
var_170.set_GridDimensions(var_171)
let (var_172: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_170.set_BlockDimensions(var_172)
let (var_173: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_170.RunAsync(var_173, var_169)
let (var_174: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_60: (Union0 ref)))
let (var_175: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_96: (Union0 ref)))
let (var_176: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_76: (Union0 ref)))
let (var_177: (float32 ref)) = (ref 1.000000f)
let (var_178: (float32 ref)) = (ref 1.000000f)
let (var_179: int32) = 6
let (var_180: int32) = 4
let (var_181: int32) = 2
let (var_182: int32) = 2
let (var_183: int32) = 2
let (var_184: int32) = 6
method_21((var_56: ManagedCuda.CudaBlas.CudaBlasHandle), (var_179: int32), (var_180: int32), (var_181: int32), (var_177: (float32 ref)), (var_174: ManagedCuda.BasicTypes.CUdeviceptr), (var_182: int32), (var_175: ManagedCuda.BasicTypes.CUdeviceptr), (var_183: int32), (var_178: (float32 ref)), (var_176: ManagedCuda.BasicTypes.CUdeviceptr), (var_184: int32))
var_127 := Union0Case1
var_117 := Union0Case1
var_106 := Union0Case1
var_96 := Union0Case1
var_82 := Union0Case1
var_76 := Union0Case1
var_71 := Union0Case1
var_65 := Union0Case1
var_60 := Union0Case1
var_55.Dispose()
var_51.Dispose()
var_49.Dispose()
let (var_185: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_43: (Union0 ref)))
var_1.FreeMemory(var_185)
var_43 := Union0Case1
var_1.Dispose()

