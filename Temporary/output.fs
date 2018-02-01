module SpiralExample.Main
let cuda_kernels = """
#include "cub/cub.cuh"

extern "C" {
    __global__ void method_14(float * var_0, float * var_1, float * var_2);
    __global__ void method_17(float * var_0, float * var_1);
    __global__ void method_19(float * var_0, float * var_1, float * var_2);
    __global__ void method_22(float var_0, float var_1, float * var_2, float * var_3, float * var_4);
    __global__ void method_23(float * var_0, float * var_1, float * var_2, float * var_3);
    __global__ void method_25(float * var_0, float * var_1);
    __device__ char method_15(long long int * var_0);
    __device__ char method_16(long long int * var_0);
    __device__ char method_18(long long int * var_0);
    __device__ char method_20(long long int * var_0, float * var_1);
    __device__ char method_26(long long int * var_0, float * var_1);
    __device__ char method_27(long long int * var_0, float * var_1);
    __device__ char method_28(long long int var_0, long long int * var_1, float * var_2);
    
    __global__ void method_14(float * var_0, float * var_1, float * var_2) {
        long long int var_3 = threadIdx.x;
        long long int var_4 = blockIdx.x;
        long long int var_5 = (10 * var_4);
        long long int var_6 = (var_3 + var_5);
        long long int var_7[1];
        var_7[0] = var_6;
        while (method_15(var_7)) {
            long long int var_9 = var_7[0];
            long long int var_10 = (var_9 + 10);
            char var_11 = (var_9 >= 0);
            char var_13;
            if (var_11) {
                var_13 = (var_9 < 10);
            } else {
                var_13 = 0;
            }
            char var_14 = (var_13 == 0);
            if (var_14) {
                // "Argument out of bounds."
            } else {
            }
            long long int var_15 = threadIdx.y;
            long long int var_16 = blockIdx.y;
            long long int var_17 = (32 * var_16);
            long long int var_18 = (var_15 + var_17);
            long long int var_19[1];
            var_19[0] = var_18;
            while (method_16(var_19)) {
                long long int var_21 = var_19[0];
                long long int var_22 = (var_21 + 32);
                char var_23 = (var_21 >= 0);
                char var_25;
                if (var_23) {
                    var_25 = (var_21 < 32);
                } else {
                    var_25 = 0;
                }
                char var_26 = (var_25 == 0);
                if (var_26) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_27 = (var_21 * 10);
                char var_29;
                if (var_11) {
                    var_29 = (var_9 < 10);
                } else {
                    var_29 = 0;
                }
                char var_30 = (var_29 == 0);
                if (var_30) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_31 = (var_27 + var_9);
                char var_33;
                if (var_23) {
                    var_33 = (var_21 < 32);
                } else {
                    var_33 = 0;
                }
                char var_34 = (var_33 == 0);
                if (var_34) {
                    // "Argument out of bounds."
                } else {
                }
                char var_36;
                if (var_11) {
                    var_36 = (var_9 < 10);
                } else {
                    var_36 = 0;
                }
                char var_37 = (var_36 == 0);
                if (var_37) {
                    // "Argument out of bounds."
                } else {
                }
                float var_38 = var_0[var_9];
                float var_39 = var_1[var_31];
                float var_40 = var_2[var_31];
                float var_41 = (var_38 + var_39);
                var_2[var_31] = var_41;
                var_19[0] = var_22;
            }
            long long int var_42 = var_19[0];
            var_7[0] = var_10;
        }
        long long int var_43 = var_7[0];
    }
    __global__ void method_17(float * var_0, float * var_1) {
        long long int var_2 = blockIdx.x;
        long long int var_3 = (var_2 * 128);
        long long int var_4 = threadIdx.x;
        long long int var_5 = (var_3 + var_4);
        long long int var_6[1];
        var_6[0] = var_5;
        while (method_18(var_6)) {
            long long int var_8 = var_6[0];
            long long int var_9 = (var_8 + 384);
            char var_10 = (var_8 >= 0);
            char var_12;
            if (var_10) {
                var_12 = (var_8 < 320);
            } else {
                var_12 = 0;
            }
            char var_13 = (var_12 == 0);
            if (var_13) {
                // "Argument out of bounds."
            } else {
            }
            char var_15;
            if (var_10) {
                var_15 = (var_8 < 320);
            } else {
                var_15 = 0;
            }
            char var_16 = (var_15 == 0);
            if (var_16) {
                // "Argument out of bounds."
            } else {
            }
            float var_17 = var_0[var_8];
            float var_18 = var_1[var_8];
            float var_19 = (-var_17);
            float var_20 = exp(var_19);
            float var_21 = (1 + var_20);
            float var_22 = (1 / var_21);
            var_1[var_8] = var_22;
            var_6[0] = var_9;
        }
        long long int var_23 = var_6[0];
    }
    __global__ void method_19(float * var_0, float * var_1, float * var_2) {
        long long int var_3 = blockIdx.x;
        long long int var_4 = (var_3 * 128);
        long long int var_5 = threadIdx.x;
        long long int var_6 = (var_4 + var_5);
        float var_7 = 0;
        long long int var_8[1];
        float var_9[1];
        var_8[0] = var_6;
        var_9[0] = var_7;
        while (method_20(var_8, var_9)) {
            long long int var_11 = var_8[0];
            float var_12 = var_9[0];
            long long int var_13 = (var_11 + 384);
            char var_14 = (var_11 >= 0);
            char var_16;
            if (var_14) {
                var_16 = (var_11 < 320);
            } else {
                var_16 = 0;
            }
            char var_17 = (var_16 == 0);
            if (var_17) {
                // "Argument out of bounds."
            } else {
            }
            float var_18 = var_0[var_11];
            float var_19 = var_1[var_11];
            float var_20 = log(var_18);
            float var_21 = (var_19 * var_20);
            float var_22 = (1 - var_19);
            float var_23 = (1 - var_18);
            float var_24 = log(var_23);
            float var_25 = (var_22 * var_24);
            float var_26 = (var_21 + var_25);
            float var_27 = (-var_26);
            float var_28 = (var_12 + var_27);
            var_8[0] = var_13;
            var_9[0] = var_28;
        }
        long long int var_29 = var_8[0];
        float var_30 = var_9[0];
        float var_31 = cub::BlockReduce<float,128,cub::BLOCK_REDUCE_WARP_REDUCTIONS,1,1>().Sum(var_30);
        long long int var_32 = threadIdx.x;
        char var_33 = (var_32 == 0);
        if (var_33) {
            long long int var_34 = blockIdx.x;
            char var_35 = (var_34 >= 0);
            char var_37;
            if (var_35) {
                var_37 = (var_34 < 3);
            } else {
                var_37 = 0;
            }
            char var_38 = (var_37 == 0);
            if (var_38) {
                // "Argument out of bounds."
            } else {
            }
            var_2[var_34] = var_31;
        } else {
        }
    }
    __global__ void method_22(float var_0, float var_1, float * var_2, float * var_3, float * var_4) {
        long long int var_5 = blockIdx.x;
        long long int var_6 = (var_5 * 128);
        long long int var_7 = threadIdx.x;
        long long int var_8 = (var_6 + var_7);
        long long int var_9[1];
        var_9[0] = var_8;
        while (method_18(var_9)) {
            long long int var_11 = var_9[0];
            long long int var_12 = (var_11 + 384);
            char var_13 = (var_11 >= 0);
            char var_15;
            if (var_13) {
                var_15 = (var_11 < 320);
            } else {
                var_15 = 0;
            }
            char var_16 = (var_15 == 0);
            if (var_16) {
                // "Argument out of bounds."
            } else {
            }
            char var_18;
            if (var_13) {
                var_18 = (var_11 < 320);
            } else {
                var_18 = 0;
            }
            char var_19 = (var_18 == 0);
            if (var_19) {
                // "Argument out of bounds."
            } else {
            }
            float var_20 = var_2[var_11];
            float var_21 = var_3[var_11];
            float var_22 = var_4[var_11];
            float var_23 = (var_20 - var_21);
            float var_24 = (1 - var_20);
            float var_25 = (var_20 * var_24);
            float var_26 = (var_23 / var_25);
            float var_27 = (var_0 * var_26);
            float var_28 = (var_22 + var_27);
            var_4[var_11] = var_28;
            var_9[0] = var_12;
        }
        long long int var_29 = var_9[0];
    }
    __global__ void method_23(float * var_0, float * var_1, float * var_2, float * var_3) {
        long long int var_4 = blockIdx.x;
        long long int var_5 = (var_4 * 128);
        long long int var_6 = threadIdx.x;
        long long int var_7 = (var_5 + var_6);
        long long int var_8[1];
        var_8[0] = var_7;
        while (method_18(var_8)) {
            long long int var_10 = var_8[0];
            long long int var_11 = (var_10 + 384);
            char var_12 = (var_10 >= 0);
            char var_14;
            if (var_12) {
                var_14 = (var_10 < 320);
            } else {
                var_14 = 0;
            }
            char var_15 = (var_14 == 0);
            if (var_15) {
                // "Argument out of bounds."
            } else {
            }
            char var_17;
            if (var_12) {
                var_17 = (var_10 < 320);
            } else {
                var_17 = 0;
            }
            char var_18 = (var_17 == 0);
            if (var_18) {
                // "Argument out of bounds."
            } else {
            }
            float var_19 = var_0[var_10];
            float var_20 = var_1[var_10];
            float var_21 = var_2[var_10];
            float var_22 = var_3[var_10];
            float var_23 = (1 - var_21);
            float var_24 = (var_21 * var_23);
            float var_25 = (var_20 * var_24);
            float var_26 = (var_22 + var_25);
            var_3[var_10] = var_26;
            var_8[0] = var_11;
        }
        long long int var_27 = var_8[0];
    }
    __global__ void method_25(float * var_0, float * var_1) {
        long long int var_2 = threadIdx.x;
        long long int var_3 = blockIdx.x;
        long long int var_4 = (10 * var_3);
        long long int var_5 = (var_2 + var_4);
        long long int var_6[1];
        var_6[0] = var_5;
        while (method_15(var_6)) {
            long long int var_8 = var_6[0];
            long long int var_9 = (var_8 + 10);
            char var_10 = (var_8 >= 0);
            char var_12;
            if (var_10) {
                var_12 = (var_8 < 10);
            } else {
                var_12 = 0;
            }
            char var_13 = (var_12 == 0);
            if (var_13) {
                // "Argument out of bounds."
            } else {
            }
            char var_15;
            if (var_10) {
                var_15 = (var_8 < 10);
            } else {
                var_15 = 0;
            }
            char var_16 = (var_15 == 0);
            if (var_16) {
                // "Argument out of bounds."
            } else {
            }
            long long int var_17 = threadIdx.y;
            long long int var_18 = blockIdx.y;
            long long int var_19 = (32 * var_18);
            long long int var_20 = (var_17 + var_19);
            float var_21 = 0;
            long long int var_22[1];
            float var_23[1];
            var_22[0] = var_20;
            var_23[0] = var_21;
            while (method_26(var_22, var_23)) {
                long long int var_25 = var_22[0];
                float var_26 = var_23[0];
                long long int var_27 = (var_25 + 32);
                char var_28 = (var_25 >= 0);
                char var_30;
                if (var_28) {
                    var_30 = (var_25 < 32);
                } else {
                    var_30 = 0;
                }
                char var_31 = (var_30 == 0);
                if (var_31) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_32 = (var_25 * 10);
                char var_34;
                if (var_10) {
                    var_34 = (var_8 < 10);
                } else {
                    var_34 = 0;
                }
                char var_35 = (var_34 == 0);
                if (var_35) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_36 = (var_32 + var_8);
                float var_37 = var_0[var_36];
                float var_38 = (var_26 + var_37);
                var_22[0] = var_27;
                var_23[0] = var_38;
            }
            long long int var_39 = var_22[0];
            float var_40 = var_23[0];
            __shared__ float var_41[1023];
            long long int var_42[1];
            float var_43[1];
            var_42[0] = 32;
            var_43[0] = var_40;
            while (method_27(var_42, var_43)) {
                long long int var_45 = var_42[0];
                float var_46 = var_43[0];
                long long int var_47 = (var_45 / 2);
                long long int var_48 = threadIdx.y;
                char var_49 = (var_48 < var_45);
                char var_52;
                if (var_49) {
                    long long int var_50 = threadIdx.y;
                    var_52 = (var_50 >= var_47);
                } else {
                    var_52 = 0;
                }
                if (var_52) {
                    long long int var_53 = threadIdx.y;
                    long long int var_54 = (var_53 - 1);
                    char var_55 = (var_54 >= 0);
                    char var_57;
                    if (var_55) {
                        var_57 = (var_54 < 31);
                    } else {
                        var_57 = 0;
                    }
                    char var_58 = (var_57 == 0);
                    if (var_58) {
                        // "Argument out of bounds."
                    } else {
                    }
                    long long int var_59 = (var_54 * 33);
                    long long int var_60 = threadIdx.x;
                    char var_61 = (var_60 >= 0);
                    char var_63;
                    if (var_61) {
                        var_63 = (var_60 < 33);
                    } else {
                        var_63 = 0;
                    }
                    char var_64 = (var_63 == 0);
                    if (var_64) {
                        // "Argument out of bounds."
                    } else {
                    }
                    long long int var_65 = (var_59 + var_60);
                    var_41[var_65] = var_46;
                } else {
                }
                __syncthreads();
                long long int var_66 = threadIdx.y;
                char var_67 = (var_66 < var_47);
                float var_92;
                if (var_67) {
                    long long int var_68 = threadIdx.y;
                    long long int var_69 = (var_68 + var_47);
                    long long int var_70[1];
                    float var_71[1];
                    var_70[0] = var_69;
                    var_71[0] = var_46;
                    while (method_28(var_45, var_70, var_71)) {
                        long long int var_73 = var_70[0];
                        float var_74 = var_71[0];
                        long long int var_75 = (var_73 + var_47);
                        long long int var_76 = (var_73 - 1);
                        char var_77 = (var_76 >= 0);
                        char var_79;
                        if (var_77) {
                            var_79 = (var_76 < 31);
                        } else {
                            var_79 = 0;
                        }
                        char var_80 = (var_79 == 0);
                        if (var_80) {
                            // "Argument out of bounds."
                        } else {
                        }
                        long long int var_81 = (var_76 * 33);
                        long long int var_82 = threadIdx.x;
                        char var_83 = (var_82 >= 0);
                        char var_85;
                        if (var_83) {
                            var_85 = (var_82 < 33);
                        } else {
                            var_85 = 0;
                        }
                        char var_86 = (var_85 == 0);
                        if (var_86) {
                            // "Argument out of bounds."
                        } else {
                        }
                        long long int var_87 = (var_81 + var_82);
                        float var_88 = var_41[var_87];
                        float var_89 = (var_74 + var_88);
                        var_70[0] = var_75;
                        var_71[0] = var_89;
                    }
                    long long int var_90 = var_70[0];
                    var_92 = var_71[0];
                } else {
                    var_92 = var_46;
                }
                var_42[0] = var_47;
                var_43[0] = var_92;
            }
            long long int var_93 = var_42[0];
            float var_94 = var_43[0];
            long long int var_95 = threadIdx.y;
            char var_96 = (var_95 == 0);
            if (var_96) {
                float var_97 = var_1[var_8];
                float var_98 = (var_94 + var_97);
                var_1[var_8] = var_98;
            } else {
            }
            var_6[0] = var_9;
        }
        long long int var_99 = var_6[0];
    }
    __device__ char method_15(long long int * var_0) {
        long long int var_1 = var_0[0];
        return (var_1 < 10);
    }
    __device__ char method_16(long long int * var_0) {
        long long int var_1 = var_0[0];
        return (var_1 < 32);
    }
    __device__ char method_18(long long int * var_0) {
        long long int var_1 = var_0[0];
        return (var_1 < 320);
    }
    __device__ char method_20(long long int * var_0, float * var_1) {
        long long int var_2 = var_0[0];
        float var_3 = var_1[0];
        return (var_2 < 320);
    }
    __device__ char method_26(long long int * var_0, float * var_1) {
        long long int var_2 = var_0[0];
        float var_3 = var_1[0];
        return (var_2 < 32);
    }
    __device__ char method_27(long long int * var_0, float * var_1) {
        long long int var_2 = var_0[0];
        float var_3 = var_1[0];
        return (var_2 >= 2);
    }
    __device__ char method_28(long long int var_0, long long int * var_1, float * var_2) {
        long long int var_3 = var_1[0];
        float var_4 = var_2[0];
        return (var_3 < var_0);
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
and EnvStack2 =
    struct
    val mem_0: (Union0 ref)
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and Env3 =
    struct
    val mem_0: EnvStack2
    val mem_1: int64
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
and Tuple4 =
    struct
    val mem_0: Tuple5
    val mem_1: (uint8 [])
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
and Tuple5 =
    struct
    val mem_0: int64
    val mem_1: int64
    val mem_2: int64
    new(arg_mem_0, arg_mem_1, arg_mem_2) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1; mem_2 = arg_mem_2}
    end
and Tuple6 =
    struct
    val mem_0: int64
    val mem_1: (uint8 [])
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
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
and method_2((var_0: string)): Tuple4 =
    let (var_1: System.IO.FileMode) = System.IO.FileMode.Open
    let (var_2: System.IO.FileAccess) = System.IO.FileAccess.Read
    let (var_3: System.IO.FileShare) = System.IO.FileShare.Read
    let (var_4: System.IO.FileStream) = System.IO.File.Open(var_0, var_1, var_2, var_3)
    let (var_5: System.IO.BinaryReader) = System.IO.BinaryReader(var_4)
    let (var_6: int32) = var_5.ReadInt32()
    let (var_7: int32) = System.Net.IPAddress.NetworkToHostOrder(var_6)
    let (var_8: bool) = (var_7 = 2051)
    let (var_9: bool) = (var_8 = false)
    if var_9 then
        (failwith "Expected a 2051i32 magic number.")
    else
        ()
    let (var_10: int32) = var_5.ReadInt32()
    let (var_11: int32) = System.Net.IPAddress.NetworkToHostOrder(var_10)
    let (var_12: int32) = var_5.ReadInt32()
    let (var_13: int32) = System.Net.IPAddress.NetworkToHostOrder(var_12)
    let (var_14: int32) = var_5.ReadInt32()
    let (var_15: int32) = System.Net.IPAddress.NetworkToHostOrder(var_14)
    let (var_16: int64) = (int64 var_11)
    let (var_17: int64) = (int64 var_13)
    let (var_18: int64) = (int64 var_15)
    let (var_19: int32) = (var_11 * var_13)
    let (var_20: int32) = (var_19 * var_15)
    let (var_22: (uint8 [])) = var_5.ReadBytes(var_20)
    var_5.Dispose()
    var_4.Dispose()
    Tuple4(Tuple5(var_16, var_17, var_18), var_22)
and method_3((var_0: (uint8 [])), (var_1: (float32 [])), (var_2: int64)): unit =
    let (var_3: bool) = (var_2 < 10000L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: int64) = (var_2 * 784L)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: int64) = 0L
        method_4((var_0: (uint8 [])), (var_6: int64), (var_1: (float32 [])), (var_7: int64))
        let (var_8: int64) = (var_2 + 1L)
        method_3((var_0: (uint8 [])), (var_1: (float32 [])), (var_8: int64))
    else
        ()
and method_5((var_0: string)): Tuple6 =
    let (var_1: System.IO.FileMode) = System.IO.FileMode.Open
    let (var_2: System.IO.FileAccess) = System.IO.FileAccess.Read
    let (var_3: System.IO.FileShare) = System.IO.FileShare.Read
    let (var_4: System.IO.FileStream) = System.IO.File.Open(var_0, var_1, var_2, var_3)
    let (var_5: System.IO.BinaryReader) = System.IO.BinaryReader(var_4)
    let (var_6: int32) = var_5.ReadInt32()
    let (var_7: int32) = System.Net.IPAddress.NetworkToHostOrder(var_6)
    let (var_8: bool) = (var_7 = 2049)
    let (var_9: bool) = (var_8 = false)
    if var_9 then
        (failwith "Expected a 2049i32 magic number.")
    else
        ()
    let (var_10: int32) = var_5.ReadInt32()
    let (var_11: int32) = System.Net.IPAddress.NetworkToHostOrder(var_10)
    let (var_12: int64) = (int64 var_11)
    let (var_14: (uint8 [])) = var_5.ReadBytes(var_11)
    var_5.Dispose()
    var_4.Dispose()
    Tuple6(var_12, var_14)
and method_6((var_0: (uint8 [])), (var_1: (float32 [])), (var_2: int64)): unit =
    let (var_3: bool) = (var_2 < 10000L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: int64) = (var_2 * 10L)
        let (var_7: uint8) = var_0.[int32 var_2]
        let (var_8: int64) = 0L
        method_7((var_7: uint8), (var_1: (float32 [])), (var_6: int64), (var_8: int64))
        let (var_9: int64) = (var_2 + 1L)
        method_6((var_0: (uint8 [])), (var_1: (float32 [])), (var_9: int64))
    else
        ()
and method_8((var_0: (uint8 [])), (var_1: (float32 [])), (var_2: int64)): unit =
    let (var_3: bool) = (var_2 < 60000L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: int64) = (var_2 * 784L)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: int64) = 0L
        method_4((var_0: (uint8 [])), (var_6: int64), (var_1: (float32 [])), (var_7: int64))
        let (var_8: int64) = (var_2 + 1L)
        method_8((var_0: (uint8 [])), (var_1: (float32 [])), (var_8: int64))
    else
        ()
and method_9((var_0: (uint8 [])), (var_1: (float32 [])), (var_2: int64)): unit =
    let (var_3: bool) = (var_2 < 60000L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: int64) = (var_2 * 10L)
        let (var_7: uint8) = var_0.[int32 var_2]
        let (var_8: int64) = 0L
        method_7((var_7: uint8), (var_1: (float32 [])), (var_6: int64), (var_8: int64))
        let (var_9: int64) = (var_2 + 1L)
        method_9((var_0: (uint8 [])), (var_1: (float32 [])), (var_9: int64))
    else
        ()
and method_10((var_0: uint64), (var_1: System.Collections.Generic.Stack<Env3>), (var_2: uint64), (var_3: int64)): EnvStack2 =
    let (var_4: int32) = var_1.get_Count()
    let (var_5: bool) = (var_4 > 0)
    if var_5 then
        let (var_6: Env3) = var_1.Peek()
        let (var_7: EnvStack2) = var_6.mem_0
        let (var_8: int64) = var_6.mem_1
        let (var_9: (Union0 ref)) = var_7.mem_0
        let (var_10: Union0) = (!var_9)
        match var_10 with
        | Union0Case0(var_11) ->
            let (var_12: ManagedCuda.BasicTypes.CUdeviceptr) = var_11.mem_0
            method_11((var_12: ManagedCuda.BasicTypes.CUdeviceptr), (var_0: uint64), (var_2: uint64), (var_3: int64), (var_1: System.Collections.Generic.Stack<Env3>), (var_7: EnvStack2), (var_8: int64))
        | Union0Case1 ->
            let (var_14: Env3) = var_1.Pop()
            let (var_15: EnvStack2) = var_14.mem_0
            let (var_16: int64) = var_14.mem_1
            method_10((var_0: uint64), (var_1: System.Collections.Generic.Stack<Env3>), (var_2: uint64), (var_3: int64))
    else
        method_12((var_0: uint64), (var_2: uint64), (var_3: int64), (var_1: System.Collections.Generic.Stack<Env3>))
and method_13((var_0: ManagedCuda.CudaBlas.CudaBlasHandle), (var_1: EnvStack2), (var_2: EnvStack2), (var_3: EnvStack2)): unit =
    let (var_4: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_5: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_6: (float32 ref)) = (ref 1.000000f)
    let (var_7: (Union0 ref)) = var_1.mem_0
    let (var_8: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_7: (Union0 ref)))
    let (var_9: (Union0 ref)) = var_2.mem_0
    let (var_10: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_9: (Union0 ref)))
    let (var_11: (float32 ref)) = (ref 0.000000f)
    let (var_12: (Union0 ref)) = var_3.mem_0
    let (var_13: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_12: (Union0 ref)))
    let (var_14: ManagedCuda.CudaBlas.CublasStatus) = ManagedCuda.CudaBlas.CudaBlasNativeMethods.cublasSgemm_v2(var_0, var_4, var_5, 10, 32, 784, var_6, var_8, 10, var_10, 784, var_11, var_13, 10)
    if var_14 <> ManagedCuda.CudaBlas.CublasStatus.Success then raise <| new ManagedCuda.CudaBlas.CudaBlasException(var_14)
and method_21((var_0: (float32 [])), (var_1: float32), (var_2: int64)): float32 =
    let (var_3: bool) = (var_2 < 3L)
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
        method_21((var_0: (float32 [])), (var_7: float32), (var_8: int64))
    else
        var_1
and method_24((var_0: ManagedCuda.CudaBlas.CudaBlasHandle), (var_1: EnvStack2), (var_2: EnvStack2), (var_3: EnvStack2)): unit =
    let (var_4: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_5: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.Transpose
    let (var_6: (float32 ref)) = (ref 1.000000f)
    let (var_7: (Union0 ref)) = var_1.mem_0
    let (var_8: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_7: (Union0 ref)))
    let (var_9: (Union0 ref)) = var_2.mem_0
    let (var_10: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_9: (Union0 ref)))
    let (var_11: (float32 ref)) = (ref 1.000000f)
    let (var_12: (Union0 ref)) = var_3.mem_0
    let (var_13: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_12: (Union0 ref)))
    let (var_14: ManagedCuda.CudaBlas.CublasStatus) = ManagedCuda.CudaBlas.CudaBlasNativeMethods.cublasSgemm_v2(var_0, var_4, var_5, 10, 784, 32, var_6, var_8, 10, var_10, 784, var_11, var_13, 10)
    if var_14 <> ManagedCuda.CudaBlas.CublasStatus.Success then raise <| new ManagedCuda.CudaBlas.CudaBlasException(var_14)
and method_29((var_0: EnvStack2), (var_1: ManagedCuda.CudaContext), (var_2: ManagedCuda.BasicTypes.CUmodule), (var_3: ManagedCuda.CudaStream), (var_4: uint64), (var_5: uint64), (var_6: System.Collections.Generic.Stack<Env3>), (var_7: EnvStack2), (var_8: EnvStack2), (var_9: ManagedCuda.CudaBlas.CudaBlasHandle), (var_10: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2), (var_13: int64)): unit =
    let (var_14: bool) = (var_13 < 784L)
    if var_14 then
        let (var_15: bool) = (var_13 >= 0L)
        let (var_16: bool) = (var_15 = false)
        if var_16 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_17: int64) = (var_13 * 10L)
        if var_16 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_18: int64) = 0L
        method_30((var_0: EnvStack2), (var_17: int64), (var_1: ManagedCuda.CudaContext), (var_2: ManagedCuda.BasicTypes.CUmodule), (var_3: ManagedCuda.CudaStream), (var_4: uint64), (var_5: uint64), (var_6: System.Collections.Generic.Stack<Env3>), (var_7: EnvStack2), (var_8: EnvStack2), (var_9: ManagedCuda.CudaBlas.CudaBlasHandle), (var_10: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2), (var_18: int64))
        let (var_19: int64) = (var_13 + 1L)
        method_29((var_0: EnvStack2), (var_1: ManagedCuda.CudaContext), (var_2: ManagedCuda.BasicTypes.CUmodule), (var_3: ManagedCuda.CudaStream), (var_4: uint64), (var_5: uint64), (var_6: System.Collections.Generic.Stack<Env3>), (var_7: EnvStack2), (var_8: EnvStack2), (var_9: ManagedCuda.CudaBlas.CudaBlasHandle), (var_10: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2), (var_19: int64))
    else
        ()
and method_32((var_0: EnvStack2), (var_1: ManagedCuda.CudaContext), (var_2: ManagedCuda.BasicTypes.CUmodule), (var_3: ManagedCuda.CudaStream), (var_4: uint64), (var_5: uint64), (var_6: System.Collections.Generic.Stack<Env3>), (var_7: EnvStack2), (var_8: ManagedCuda.CudaBlas.CudaBlasHandle), (var_9: EnvStack2), (var_10: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2), (var_13: int64)): unit =
    let (var_14: bool) = (var_13 < 10L)
    if var_14 then
        let (var_15: bool) = (var_13 >= 0L)
        let (var_16: bool) = (var_15 = false)
        if var_16 then
            (failwith "Argument out of bounds.")
        else
            ()
        if var_16 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_17: (Union0 ref)) = var_7.mem_0
        let (var_18: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_17: (Union0 ref)))
        let (var_19: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(4L)
        let (var_20: ManagedCuda.CudaDeviceVariable<float32>) = ManagedCuda.CudaDeviceVariable<float32>(var_18, false, var_19)
        let (var_21: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_13)
        let (var_22: float32) = var_20.[var_21]
        let (var_23: float32) = (var_22 + 0.001000f)
        let (var_24: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_13)
        var_20.[var_24] <- var_23
        let (var_25: float32) = method_31((var_1: ManagedCuda.CudaContext), (var_2: ManagedCuda.BasicTypes.CUmodule), (var_3: ManagedCuda.CudaStream), (var_4: uint64), (var_5: uint64), (var_6: System.Collections.Generic.Stack<Env3>), (var_0: EnvStack2), (var_7: EnvStack2), (var_8: ManagedCuda.CudaBlas.CudaBlasHandle), (var_9: EnvStack2), (var_10: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2))
        let (var_26: float32) = (var_22 - 0.001000f)
        let (var_27: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_13)
        var_20.[var_27] <- var_26
        let (var_28: float32) = method_31((var_1: ManagedCuda.CudaContext), (var_2: ManagedCuda.BasicTypes.CUmodule), (var_3: ManagedCuda.CudaStream), (var_4: uint64), (var_5: uint64), (var_6: System.Collections.Generic.Stack<Env3>), (var_0: EnvStack2), (var_7: EnvStack2), (var_8: ManagedCuda.CudaBlas.CudaBlasHandle), (var_9: EnvStack2), (var_10: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2))
        let (var_29: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_13)
        var_20.[var_29] <- var_22
        let (var_30: float32) = (var_25 - var_28)
        let (var_31: float32) = (var_30 / 0.002000f)
        let (var_32: (Union0 ref)) = var_0.mem_0
        let (var_33: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_32: (Union0 ref)))
        let (var_34: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(4L)
        let (var_35: ManagedCuda.CudaDeviceVariable<float32>) = ManagedCuda.CudaDeviceVariable<float32>(var_33, false, var_34)
        let (var_36: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_13)
        let (var_37: float32) = var_35.[var_36]
        let (var_38: float32) = (var_37 - var_31)
        let (var_39: float32) = (-var_38)
        let (var_40: bool) = (var_39 < var_38)
        let (var_41: float32) =
            if var_40 then
                var_38
            else
                var_39
        let (var_42: bool) = (var_41 >= 0.001000f)
        if var_42 then
            let (var_43: string) = System.String.Format("{0}",var_37)
            let (var_44: string) = System.String.Format("{0} = {1}","true_gradient",var_43)
            let (var_45: string) = System.String.Format("{0}",var_41)
            let (var_46: string) = System.String.Format("{0} = {1}","diff",var_45)
            let (var_47: string) = System.String.Format("{0}",var_31)
            let (var_48: string) = System.String.Format("{0} = {1}","approx_gradient",var_47)
            let (var_49: string) = String.concat "; " [|var_48; var_46; var_44|]
            let (var_50: string) = System.String.Format("{0}{1}{2}","{",var_49,"}")
            System.Console.WriteLine(var_50)
            System.Console.WriteLine("--- Gradient checking failure.")
        else
            ()
        let (var_51: int64) = (var_13 + 1L)
        method_32((var_0: EnvStack2), (var_1: ManagedCuda.CudaContext), (var_2: ManagedCuda.BasicTypes.CUmodule), (var_3: ManagedCuda.CudaStream), (var_4: uint64), (var_5: uint64), (var_6: System.Collections.Generic.Stack<Env3>), (var_7: EnvStack2), (var_8: ManagedCuda.CudaBlas.CudaBlasHandle), (var_9: EnvStack2), (var_10: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2), (var_51: int64))
    else
        ()
and method_4((var_0: (uint8 [])), (var_1: int64), (var_2: (float32 [])), (var_3: int64)): unit =
    let (var_4: bool) = (var_3 < 784L)
    if var_4 then
        let (var_5: bool) = (var_3 >= 0L)
        let (var_6: bool) = (var_5 = false)
        if var_6 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: int64) = (var_1 + var_3)
        if var_6 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_8: uint8) = var_0.[int32 var_7]
        let (var_9: float32) = (float32 var_8)
        let (var_10: float32) = (var_9 / 255.000000f)
        var_2.[int32 var_7] <- var_10
        let (var_11: int64) = (var_3 + 1L)
        method_4((var_0: (uint8 [])), (var_1: int64), (var_2: (float32 [])), (var_11: int64))
    else
        ()
and method_7((var_0: uint8), (var_1: (float32 [])), (var_2: int64), (var_3: int64)): unit =
    let (var_4: bool) = (var_3 < 10L)
    if var_4 then
        let (var_5: bool) = (var_3 >= 0L)
        let (var_6: bool) = (var_5 = false)
        if var_6 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: int64) = (var_2 + var_3)
        let (var_8: uint8) = (uint8 var_3)
        let (var_9: bool) = (var_8 = var_0)
        let (var_10: float32) =
            if var_9 then
                1.000000f
            else
                0.000000f
        var_1.[int32 var_7] <- var_10
        let (var_11: int64) = (var_3 + 1L)
        method_7((var_0: uint8), (var_1: (float32 [])), (var_2: int64), (var_11: int64))
    else
        ()
and method_11((var_0: ManagedCuda.BasicTypes.CUdeviceptr), (var_1: uint64), (var_2: uint64), (var_3: int64), (var_4: System.Collections.Generic.Stack<Env3>), (var_5: EnvStack2), (var_6: int64)): EnvStack2 =
    let (var_7: ManagedCuda.BasicTypes.SizeT) = var_0.Pointer
    let (var_8: uint64) = uint64 var_7
    let (var_9: uint64) = uint64 var_6
    let (var_10: int64) = (var_3 % 256L)
    let (var_11: int64) = (var_3 - var_10)
    let (var_12: int64) = (var_11 + 256L)
    let (var_13: uint64) = (var_8 + var_9)
    let (var_14: uint64) = (var_1 + var_2)
    let (var_15: uint64) = uint64 var_12
    let (var_16: uint64) = (var_14 - var_13)
    let (var_17: bool) = (var_15 <= var_16)
    let (var_18: bool) = (var_17 = false)
    if var_18 then
        (failwith "Cache size has been exceeded in the allocator.")
    else
        ()
    let (var_19: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_13)
    let (var_20: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_19)
    let (var_21: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_20))))
    let (var_22: EnvStack2) = EnvStack2((var_21: (Union0 ref)))
    var_4.Push((Env3(var_22, var_12)))
    var_22
and method_12((var_0: uint64), (var_1: uint64), (var_2: int64), (var_3: System.Collections.Generic.Stack<Env3>)): EnvStack2 =
    let (var_4: int64) = (var_2 % 256L)
    let (var_5: int64) = (var_2 - var_4)
    let (var_6: int64) = (var_5 + 256L)
    let (var_7: uint64) = (var_0 + var_1)
    let (var_8: uint64) = uint64 var_6
    let (var_9: uint64) = (var_7 - var_0)
    let (var_10: bool) = (var_8 <= var_9)
    let (var_11: bool) = (var_10 = false)
    if var_11 then
        (failwith "Cache size has been exceeded in the allocator.")
    else
        ()
    let (var_12: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_0)
    let (var_13: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_12)
    let (var_14: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_13))))
    let (var_15: EnvStack2) = EnvStack2((var_14: (Union0 ref)))
    var_3.Push((Env3(var_15, var_6)))
    var_15
and method_30((var_0: EnvStack2), (var_1: int64), (var_2: ManagedCuda.CudaContext), (var_3: ManagedCuda.BasicTypes.CUmodule), (var_4: ManagedCuda.CudaStream), (var_5: uint64), (var_6: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_8: EnvStack2), (var_9: EnvStack2), (var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_11: EnvStack2), (var_12: EnvStack2), (var_13: EnvStack2), (var_14: int64)): unit =
    let (var_15: bool) = (var_14 < 10L)
    if var_15 then
        let (var_16: bool) = (var_14 >= 0L)
        let (var_17: bool) = (var_16 = false)
        if var_17 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_18: int64) = (var_1 + var_14)
        if var_17 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_19: (Union0 ref)) = var_11.mem_0
        let (var_20: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_19: (Union0 ref)))
        let (var_21: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(4L)
        let (var_22: ManagedCuda.CudaDeviceVariable<float32>) = ManagedCuda.CudaDeviceVariable<float32>(var_20, false, var_21)
        let (var_23: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_18)
        let (var_24: float32) = var_22.[var_23]
        let (var_25: float32) = (var_24 + 0.001000f)
        let (var_26: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_18)
        var_22.[var_26] <- var_25
        let (var_27: float32) = method_31((var_2: ManagedCuda.CudaContext), (var_3: ManagedCuda.BasicTypes.CUmodule), (var_4: ManagedCuda.CudaStream), (var_5: uint64), (var_6: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_8: EnvStack2), (var_9: EnvStack2), (var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_0: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2), (var_13: EnvStack2))
        let (var_28: float32) = (var_24 - 0.001000f)
        let (var_29: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_18)
        var_22.[var_29] <- var_28
        let (var_30: float32) = method_31((var_2: ManagedCuda.CudaContext), (var_3: ManagedCuda.BasicTypes.CUmodule), (var_4: ManagedCuda.CudaStream), (var_5: uint64), (var_6: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_8: EnvStack2), (var_9: EnvStack2), (var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_0: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2), (var_13: EnvStack2))
        let (var_31: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_18)
        var_22.[var_31] <- var_24
        let (var_32: float32) = (var_27 - var_30)
        let (var_33: float32) = (var_32 / 0.002000f)
        let (var_34: (Union0 ref)) = var_0.mem_0
        let (var_35: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_34: (Union0 ref)))
        let (var_36: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(4L)
        let (var_37: ManagedCuda.CudaDeviceVariable<float32>) = ManagedCuda.CudaDeviceVariable<float32>(var_35, false, var_36)
        let (var_38: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_18)
        let (var_39: float32) = var_37.[var_38]
        let (var_40: float32) = (var_39 - var_33)
        let (var_41: float32) = (-var_40)
        let (var_42: bool) = (var_41 < var_40)
        let (var_43: float32) =
            if var_42 then
                var_40
            else
                var_41
        let (var_44: bool) = (var_43 >= 0.001000f)
        if var_44 then
            let (var_45: string) = System.String.Format("{0}",var_39)
            let (var_46: string) = System.String.Format("{0} = {1}","true_gradient",var_45)
            let (var_47: string) = System.String.Format("{0}",var_43)
            let (var_48: string) = System.String.Format("{0} = {1}","diff",var_47)
            let (var_49: string) = System.String.Format("{0}",var_33)
            let (var_50: string) = System.String.Format("{0} = {1}","approx_gradient",var_49)
            let (var_51: string) = String.concat "; " [|var_50; var_48; var_46|]
            let (var_52: string) = System.String.Format("{0}{1}{2}","{",var_51,"}")
            System.Console.WriteLine(var_52)
            System.Console.WriteLine("--- Gradient checking failure.")
        else
            ()
        let (var_53: int64) = (var_14 + 1L)
        method_30((var_0: EnvStack2), (var_1: int64), (var_2: ManagedCuda.CudaContext), (var_3: ManagedCuda.BasicTypes.CUmodule), (var_4: ManagedCuda.CudaStream), (var_5: uint64), (var_6: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_8: EnvStack2), (var_9: EnvStack2), (var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_11: EnvStack2), (var_12: EnvStack2), (var_13: EnvStack2), (var_53: int64))
    else
        ()
and method_31((var_0: ManagedCuda.CudaContext), (var_1: ManagedCuda.BasicTypes.CUmodule), (var_2: ManagedCuda.CudaStream), (var_3: uint64), (var_4: uint64), (var_5: System.Collections.Generic.Stack<Env3>), (var_6: EnvStack2), (var_7: EnvStack2), (var_8: ManagedCuda.CudaBlas.CudaBlasHandle), (var_9: EnvStack2), (var_10: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2)): float32 =
    let (var_13: int64) = 1280L
    let (var_14: EnvStack2) = method_10((var_3: uint64), (var_5: System.Collections.Generic.Stack<Env3>), (var_4: uint64), (var_13: int64))
    method_13((var_8: ManagedCuda.CudaBlas.CudaBlasHandle), (var_10: EnvStack2), (var_11: EnvStack2), (var_14: EnvStack2))
    let (var_15: int64) = 1280L
    let (var_16: EnvStack2) = method_10((var_3: uint64), (var_5: System.Collections.Generic.Stack<Env3>), (var_4: uint64), (var_15: int64))
    let (var_17: (Union0 ref)) = var_16.mem_0
    let (var_18: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_17: (Union0 ref)))
    let (var_19: ManagedCuda.BasicTypes.CUstream) = var_2.get_Stream()
    let (var_20: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(1280L)
    var_0.ClearMemoryAsync(var_18, 0uy, var_20, var_19)
    let (var_21: (Union0 ref)) = var_7.mem_0
    let (var_22: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_21: (Union0 ref)))
    let (var_23: (Union0 ref)) = var_14.mem_0
    let (var_24: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_23: (Union0 ref)))
    let (var_25: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_23: (Union0 ref)))
    // Cuda join point
    // method_14((var_22: ManagedCuda.BasicTypes.CUdeviceptr), (var_24: ManagedCuda.BasicTypes.CUdeviceptr), (var_25: ManagedCuda.BasicTypes.CUdeviceptr))
    let (var_26: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_14", var_1, var_0)
    let (var_27: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
    var_26.set_GridDimensions(var_27)
    let (var_28: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(10u, 32u, 1u)
    var_26.set_BlockDimensions(var_28)
    let (var_29: ManagedCuda.BasicTypes.CUstream) = var_2.get_Stream()
    let (var_31: (System.Object [])) = [|var_22; var_24; var_25|]: (System.Object [])
    var_26.RunAsync(var_29, var_31)
    let (var_36: int64) = 1280L
    let (var_37: EnvStack2) = method_10((var_3: uint64), (var_5: System.Collections.Generic.Stack<Env3>), (var_4: uint64), (var_36: int64))
    let (var_38: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_23: (Union0 ref)))
    let (var_39: (Union0 ref)) = var_37.mem_0
    let (var_40: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_39: (Union0 ref)))
    // Cuda join point
    // method_17((var_38: ManagedCuda.BasicTypes.CUdeviceptr), (var_40: ManagedCuda.BasicTypes.CUdeviceptr))
    let (var_41: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_17", var_1, var_0)
    let (var_42: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(3u, 1u, 1u)
    var_41.set_GridDimensions(var_42)
    let (var_43: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
    var_41.set_BlockDimensions(var_43)
    let (var_44: ManagedCuda.BasicTypes.CUstream) = var_2.get_Stream()
    let (var_46: (System.Object [])) = [|var_38; var_40|]: (System.Object [])
    var_41.RunAsync(var_44, var_46)
    let (var_47: int64) = 1280L
    let (var_48: EnvStack2) = method_10((var_3: uint64), (var_5: System.Collections.Generic.Stack<Env3>), (var_4: uint64), (var_47: int64))
    let (var_49: (Union0 ref)) = var_48.mem_0
    let (var_50: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_49: (Union0 ref)))
    let (var_51: ManagedCuda.BasicTypes.CUstream) = var_2.get_Stream()
    let (var_52: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(1280L)
    var_0.ClearMemoryAsync(var_50, 0uy, var_52, var_51)
    let (var_53: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_39: (Union0 ref)))
    let (var_54: (Union0 ref)) = var_12.mem_0
    let (var_55: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_54: (Union0 ref)))
    let (var_63: int64) = 12L
    let (var_64: EnvStack2) = method_10((var_3: uint64), (var_5: System.Collections.Generic.Stack<Env3>), (var_4: uint64), (var_63: int64))
    let (var_65: (Union0 ref)) = var_64.mem_0
    let (var_66: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_65: (Union0 ref)))
    // Cuda join point
    // method_19((var_53: ManagedCuda.BasicTypes.CUdeviceptr), (var_55: ManagedCuda.BasicTypes.CUdeviceptr), (var_66: ManagedCuda.BasicTypes.CUdeviceptr))
    let (var_67: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_19", var_1, var_0)
    let (var_68: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(3u, 1u, 1u)
    var_67.set_GridDimensions(var_68)
    let (var_69: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
    var_67.set_BlockDimensions(var_69)
    let (var_70: ManagedCuda.BasicTypes.CUstream) = var_2.get_Stream()
    let (var_72: (System.Object [])) = [|var_53; var_55; var_66|]: (System.Object [])
    var_67.RunAsync(var_70, var_72)
    let (var_73: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(3L))
    let (var_74: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_65: (Union0 ref)))
    var_0.CopyToHost(var_73, var_74)
    let (var_75: float32) = 0.000000f
    let (var_76: int64) = 0L
    let (var_77: float32) = method_21((var_73: (float32 [])), (var_75: float32), (var_76: int64))
    var_65 := Union0Case1
    let (var_78: (float32 ref)) = (ref 0.000000f)
    let (var_79: float32) = (var_77 / 32.000000f)
    let (var_80: (float32 ref)) = (ref 0.000000f)
    var_49 := Union0Case1
    var_39 := Union0Case1
    var_17 := Union0Case1
    var_23 := Union0Case1
    var_79
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
let (var_8: string) = System.IO.Path.Combine("C:/Program Files (x86)/Microsoft Visual Studio/2017/Community", "VC/Auxiliary/Build/vcvars64.bat")
let (var_9: string) = System.IO.Path.Combine("C:/Program Files (x86)/Microsoft Visual Studio/2017/Community", "VC/Tools/MSVC/14.11.25503/bin/Hostx64/x64")
let (var_10: string) = System.IO.Path.Combine("C:/Program Files/NVIDIA GPU Computing Toolkit/CUDA/v9.0", "include")
let (var_11: string) = System.IO.Path.Combine("C:/Program Files (x86)/Microsoft Visual Studio/2017/Community", "VC/Tools/MSVC/14.11.25503/include")
let (var_12: string) = System.IO.Path.Combine("C:/Program Files/NVIDIA GPU Computing Toolkit/CUDA/v9.0", "bin/nvcc.exe")
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
let (var_21: string) = String.concat "" [|"\""; var_12; "\" -gencode=arch=compute_52,code=\\\"sm_52,compute_52\\\" --use-local-env --cl-version 2017 -I\""; var_10; "\" -I\"C:/cub-1.7.4\" -I\""; var_11; "\" --keep-dir \""; var_2; "\" -maxrregcount=0  --machine 64 -ptx -cudart static  -o \""; var_13; "\" \""; var_14; "\""|]
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
let (var_44: EnvStack2) = EnvStack2((var_43: (Union0 ref)))
let (var_45: System.Collections.Generic.Stack<Env3>) = System.Collections.Generic.Stack<Env3>()
let (var_46: (Union0 ref)) = var_44.mem_0
let (var_47: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_46: (Union0 ref)))
let (var_48: ManagedCuda.BasicTypes.SizeT) = var_47.Pointer
let (var_49: uint64) = uint64 var_48
let (var_50: uint64) = uint64 var_40
let (var_51: ManagedCuda.CudaStream) = ManagedCuda.CudaStream()
let (var_52: ManagedCuda.CudaRand.GeneratorType) = ManagedCuda.CudaRand.GeneratorType.PseudoDefault
let (var_53: ManagedCuda.CudaRand.CudaRandDevice) = ManagedCuda.CudaRand.CudaRandDevice(var_52)
let (var_54: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
var_53.SetStream(var_54)
let (var_55: ManagedCuda.CudaBlas.PointerMode) = ManagedCuda.CudaBlas.PointerMode.Host
let (var_56: ManagedCuda.CudaBlas.AtomicsMode) = ManagedCuda.CudaBlas.AtomicsMode.Allowed
let (var_57: ManagedCuda.CudaBlas.CudaBlas) = ManagedCuda.CudaBlas.CudaBlas(var_55, var_56)
let (var_58: ManagedCuda.CudaBlas.CudaBlasHandle) = var_57.get_CublasHandle()
let (var_59: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
var_57.set_Stream(var_59)
let (var_60: string) = System.IO.Path.Combine("C:\\ML Datasets\\Mnist", "t10k-images.idx3-ubyte")
let (var_61: Tuple4) = method_2((var_60: string))
let (var_62: Tuple5) = var_61.mem_0
let (var_63: (uint8 [])) = var_61.mem_1
let (var_64: int64) = var_62.mem_0
let (var_65: int64) = var_62.mem_1
let (var_66: int64) = var_62.mem_2
let (var_67: bool) = (10000L = var_64)
let (var_71: bool) =
    if var_67 then
        let (var_68: bool) = (28L = var_65)
        if var_68 then
            (28L = var_66)
        else
            false
    else
        false
let (var_72: bool) = (var_71 = false)
if var_72 then
    (failwith "Mnist dimensions do not match the expected values.")
else
    ()
let (var_73: int64) = var_63.LongLength
let (var_74: bool) = (7840000L = var_73)
let (var_75: bool) = (var_74 = false)
if var_75 then
    (failwith "The product of dimensions of the new tensor must equal that of the previous one.")
else
    ()
let (var_79: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(7840000L))
let (var_80: int64) = 0L
method_3((var_63: (uint8 [])), (var_79: (float32 [])), (var_80: int64))
let (var_81: string) = System.IO.Path.Combine("C:\\ML Datasets\\Mnist", "t10k-labels.idx1-ubyte")
let (var_82: Tuple6) = method_5((var_81: string))
let (var_83: int64) = var_82.mem_0
let (var_84: (uint8 [])) = var_82.mem_1
let (var_85: bool) = (10000L = var_83)
let (var_86: bool) = (var_85 = false)
if var_86 then
    (failwith "Mnist dimensions do not match the expected values.")
else
    ()
let (var_90: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(100000L))
let (var_91: int64) = 0L
method_6((var_84: (uint8 [])), (var_90: (float32 [])), (var_91: int64))
let (var_92: string) = System.IO.Path.Combine("C:\\ML Datasets\\Mnist", "train-images.idx3-ubyte")
let (var_93: Tuple4) = method_2((var_92: string))
let (var_94: Tuple5) = var_93.mem_0
let (var_95: (uint8 [])) = var_93.mem_1
let (var_96: int64) = var_94.mem_0
let (var_97: int64) = var_94.mem_1
let (var_98: int64) = var_94.mem_2
let (var_99: bool) = (60000L = var_96)
let (var_103: bool) =
    if var_99 then
        let (var_100: bool) = (28L = var_97)
        if var_100 then
            (28L = var_98)
        else
            false
    else
        false
let (var_104: bool) = (var_103 = false)
if var_104 then
    (failwith "Mnist dimensions do not match the expected values.")
else
    ()
let (var_105: int64) = var_95.LongLength
let (var_106: bool) = (47040000L = var_105)
let (var_107: bool) = (var_106 = false)
if var_107 then
    (failwith "The product of dimensions of the new tensor must equal that of the previous one.")
else
    ()
let (var_111: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(47040000L))
let (var_112: int64) = 0L
method_8((var_95: (uint8 [])), (var_111: (float32 [])), (var_112: int64))
let (var_113: string) = System.IO.Path.Combine("C:\\ML Datasets\\Mnist", "train-labels.idx1-ubyte")
let (var_114: Tuple6) = method_5((var_113: string))
let (var_115: int64) = var_114.mem_0
let (var_116: (uint8 [])) = var_114.mem_1
let (var_117: bool) = (60000L = var_115)
let (var_118: bool) = (var_117 = false)
if var_118 then
    (failwith "Mnist dimensions do not match the expected values.")
else
    ()
let (var_122: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(600000L))
let (var_123: int64) = 0L
method_9((var_116: (uint8 [])), (var_122: (float32 [])), (var_123: int64))
let (var_124: int64) = var_79.LongLength
let (var_125: int64) = (var_124 * 4L)
let (var_126: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_125: int64))
let (var_127: (Union0 ref)) = var_126.mem_0
let (var_128: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_127: (Union0 ref)))
var_1.CopyToDevice(var_128, var_79)
let (var_129: int64) = var_90.LongLength
let (var_130: int64) = (var_129 * 4L)
let (var_131: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_130: int64))
let (var_132: (Union0 ref)) = var_131.mem_0
let (var_133: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_132: (Union0 ref)))
var_1.CopyToDevice(var_133, var_90)
let (var_134: int64) = var_111.LongLength
let (var_135: int64) = (var_134 * 4L)
let (var_136: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_135: int64))
let (var_137: (Union0 ref)) = var_136.mem_0
let (var_138: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_137: (Union0 ref)))
var_1.CopyToDevice(var_138, var_111)
let (var_139: int64) = var_122.LongLength
let (var_140: int64) = (var_139 * 4L)
let (var_141: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_140: int64))
let (var_142: (Union0 ref)) = var_141.mem_0
let (var_143: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_142: (Union0 ref)))
var_1.CopyToDevice(var_143, var_122)
let (var_144: int64) = 31360L
let (var_145: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_144: int64))
let (var_146: (Union0 ref)) = var_145.mem_0
let (var_147: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_146: (Union0 ref)))
let (var_148: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(7840L)
var_53.GenerateNormal32(var_147, var_148, 0.000000f, 0.050189f)
let (var_149: int64) = 31360L
let (var_150: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_149: int64))
let (var_151: (Union0 ref)) = var_150.mem_0
let (var_152: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_151: (Union0 ref)))
let (var_153: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_154: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(31360L)
var_1.ClearMemoryAsync(var_152, 0uy, var_154, var_153)
let (var_155: int64) = 40L
let (var_156: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_155: int64))
let (var_157: (Union0 ref)) = var_156.mem_0
let (var_158: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_157: (Union0 ref)))
let (var_159: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_160: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(40L)
var_1.ClearMemoryAsync(var_158, 0uy, var_160, var_159)
let (var_161: int64) = 40L
let (var_162: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_161: int64))
let (var_163: (Union0 ref)) = var_162.mem_0
let (var_164: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_163: (Union0 ref)))
let (var_165: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_166: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(40L)
var_1.ClearMemoryAsync(var_164, 0uy, var_166, var_165)
let (var_167: int64) = 1280L
let (var_168: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_167: int64))
method_13((var_58: ManagedCuda.CudaBlas.CudaBlasHandle), (var_145: EnvStack2), (var_136: EnvStack2), (var_168: EnvStack2))
let (var_169: int64) = 1280L
let (var_170: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_169: int64))
let (var_171: (Union0 ref)) = var_170.mem_0
let (var_172: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_171: (Union0 ref)))
let (var_173: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_174: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(1280L)
var_1.ClearMemoryAsync(var_172, 0uy, var_174, var_173)
let (var_175: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_157: (Union0 ref)))
let (var_176: (Union0 ref)) = var_168.mem_0
let (var_177: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_176: (Union0 ref)))
let (var_178: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_176: (Union0 ref)))
// Cuda join point
// method_14((var_175: ManagedCuda.BasicTypes.CUdeviceptr), (var_177: ManagedCuda.BasicTypes.CUdeviceptr), (var_178: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_179: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_14", var_32, var_1)
let (var_180: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
var_179.set_GridDimensions(var_180)
let (var_181: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(10u, 32u, 1u)
var_179.set_BlockDimensions(var_181)
let (var_182: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_184: (System.Object [])) = [|var_175; var_177; var_178|]: (System.Object [])
var_179.RunAsync(var_182, var_184)
let (var_189: int64) = 1280L
let (var_190: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_189: int64))
let (var_191: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_176: (Union0 ref)))
let (var_192: (Union0 ref)) = var_190.mem_0
let (var_193: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_192: (Union0 ref)))
// Cuda join point
// method_17((var_191: ManagedCuda.BasicTypes.CUdeviceptr), (var_193: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_194: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_17", var_32, var_1)
let (var_195: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(3u, 1u, 1u)
var_194.set_GridDimensions(var_195)
let (var_196: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_194.set_BlockDimensions(var_196)
let (var_197: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_199: (System.Object [])) = [|var_191; var_193|]: (System.Object [])
var_194.RunAsync(var_197, var_199)
let (var_200: int64) = 1280L
let (var_201: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_200: int64))
let (var_202: (Union0 ref)) = var_201.mem_0
let (var_203: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_202: (Union0 ref)))
let (var_204: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_205: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(1280L)
var_1.ClearMemoryAsync(var_203, 0uy, var_205, var_204)
let (var_206: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_192: (Union0 ref)))
let (var_207: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_142: (Union0 ref)))
let (var_215: int64) = 12L
let (var_216: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_215: int64))
let (var_217: (Union0 ref)) = var_216.mem_0
let (var_218: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_217: (Union0 ref)))
// Cuda join point
// method_19((var_206: ManagedCuda.BasicTypes.CUdeviceptr), (var_207: ManagedCuda.BasicTypes.CUdeviceptr), (var_218: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_219: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_19", var_32, var_1)
let (var_220: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(3u, 1u, 1u)
var_219.set_GridDimensions(var_220)
let (var_221: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_219.set_BlockDimensions(var_221)
let (var_222: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_224: (System.Object [])) = [|var_206; var_207; var_218|]: (System.Object [])
var_219.RunAsync(var_222, var_224)
let (var_225: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(3L))
let (var_226: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_217: (Union0 ref)))
var_1.CopyToHost(var_225, var_226)
let (var_227: float32) = 0.000000f
let (var_228: int64) = 0L
let (var_229: float32) = method_21((var_225: (float32 [])), (var_227: float32), (var_228: int64))
var_217 := Union0Case1
let (var_230: (float32 ref)) = (ref 0.000000f)
let (var_231: float32) = (var_229 / 32.000000f)
let (var_232: (float32 ref)) = (ref 0.000000f)
var_232 := 1.000000f
let (var_233: float32) = (!var_232)
let (var_234: float32) = (var_233 / 32.000000f)
let (var_235: float32) = (!var_230)
let (var_236: float32) = (var_235 + var_234)
var_230 := var_236
let (var_237: float32) = (!var_230)
let (var_238: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_192: (Union0 ref)))
let (var_239: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_142: (Union0 ref)))
let (var_240: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_202: (Union0 ref)))
// Cuda join point
// method_22((var_237: float32), (var_229: float32), (var_238: ManagedCuda.BasicTypes.CUdeviceptr), (var_239: ManagedCuda.BasicTypes.CUdeviceptr), (var_240: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_241: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_22", var_32, var_1)
let (var_242: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(3u, 1u, 1u)
var_241.set_GridDimensions(var_242)
let (var_243: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_241.set_BlockDimensions(var_243)
let (var_244: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_246: (System.Object [])) = [|var_237; var_229; var_238; var_239; var_240|]: (System.Object [])
var_241.RunAsync(var_244, var_246)
let (var_247: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_176: (Union0 ref)))
let (var_248: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_202: (Union0 ref)))
let (var_249: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_192: (Union0 ref)))
let (var_250: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_171: (Union0 ref)))
// Cuda join point
// method_23((var_247: ManagedCuda.BasicTypes.CUdeviceptr), (var_248: ManagedCuda.BasicTypes.CUdeviceptr), (var_249: ManagedCuda.BasicTypes.CUdeviceptr), (var_250: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_251: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_23", var_32, var_1)
let (var_252: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(3u, 1u, 1u)
var_251.set_GridDimensions(var_252)
let (var_253: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_251.set_BlockDimensions(var_253)
let (var_254: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_256: (System.Object [])) = [|var_247; var_248; var_249; var_250|]: (System.Object [])
var_251.RunAsync(var_254, var_256)
method_24((var_58: ManagedCuda.CudaBlas.CudaBlasHandle), (var_170: EnvStack2), (var_136: EnvStack2), (var_150: EnvStack2))
let (var_257: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_171: (Union0 ref)))
let (var_258: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_163: (Union0 ref)))
// Cuda join point
// method_25((var_257: ManagedCuda.BasicTypes.CUdeviceptr), (var_258: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_259: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_25", var_32, var_1)
let (var_260: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
var_259.set_GridDimensions(var_260)
let (var_261: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(10u, 32u, 1u)
var_259.set_BlockDimensions(var_261)
let (var_262: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_264: (System.Object [])) = [|var_257; var_258|]: (System.Object [])
var_259.RunAsync(var_262, var_264)
var_202 := Union0Case1
var_192 := Union0Case1
var_171 := Union0Case1
var_176 := Union0Case1
let (var_265: int64) = 0L
method_29((var_150: EnvStack2), (var_1: ManagedCuda.CudaContext), (var_32: ManagedCuda.BasicTypes.CUmodule), (var_51: ManagedCuda.CudaStream), (var_49: uint64), (var_50: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_162: EnvStack2), (var_156: EnvStack2), (var_58: ManagedCuda.CudaBlas.CudaBlasHandle), (var_145: EnvStack2), (var_136: EnvStack2), (var_141: EnvStack2), (var_265: int64))
let (var_266: int64) = 0L
method_32((var_162: EnvStack2), (var_1: ManagedCuda.CudaContext), (var_32: ManagedCuda.BasicTypes.CUmodule), (var_51: ManagedCuda.CudaStream), (var_49: uint64), (var_50: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_156: EnvStack2), (var_58: ManagedCuda.CudaBlas.CudaBlasHandle), (var_150: EnvStack2), (var_145: EnvStack2), (var_136: EnvStack2), (var_141: EnvStack2), (var_266: int64))
var_163 := Union0Case1
var_157 := Union0Case1
var_151 := Union0Case1
var_146 := Union0Case1
var_57.Dispose()
var_53.Dispose()
var_51.Dispose()
let (var_267: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_46: (Union0 ref)))
var_1.FreeMemory(var_267)
var_46 := Union0Case1
var_1.Dispose()

