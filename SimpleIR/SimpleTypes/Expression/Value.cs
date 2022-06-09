using System;
using LLVMSharp;

namespace SimpleIR.SimpleTypes.Expression
{
    public class Value : SimpleType
    {
        public DataTypeKind Kind;
        public object Literal;

        public Value(object literal, DataTypeKind kind)
        {
            Literal = literal;
            Kind = kind;
        }

        public object Emit(Module module)
        {
            switch (Kind)
            {
                case DataTypeKind.IntPtr:
                {
                    throw new NotImplementedException("Ptrs not yet implemented.");
                }
                case DataTypeKind.Int64:
                {
                    return LLVM.ConstInt(LLVM.Int64Type(), ulong.Parse(Literal.ToString()), new LLVMBool());
                }
                case DataTypeKind.Int16:
                {
                    return LLVM.ConstInt(LLVM.Int16Type(), ulong.Parse(Literal.ToString()), new LLVMBool());
                }
                case DataTypeKind.Float:
                {
                    return LLVM.ConstInt(LLVM.FloatType(), ulong.Parse(Literal.ToString()), new LLVMBool());
                }
                case DataTypeKind.FP128:
                {
                    return LLVM.ConstInt(LLVM.FP128Type(), ulong.Parse(Literal.ToString()), new LLVMBool());
                }
                case DataTypeKind.Int8:
                {
                    return LLVM.ConstInt(LLVM.Int8Type(), ulong.Parse(Literal.ToString()), new LLVMBool());
                }
                case DataTypeKind.Int1:
                case DataTypeKind.Boolean:
                {
                    return LLVM.ConstInt(LLVM.Int1Type(), ulong.Parse(Literal.ToString()), new LLVMBool());
                }
                case DataTypeKind.Int32:
                case DataTypeKind.Number:
                {
                    return LLVM.ConstInt(LLVM.Int32Type(), ulong.Parse(Literal.ToString()), new LLVMBool());
                }
                case DataTypeKind.String:
                {
                    if (!(Literal is SimpleType))
                        return LLVM.BuildGlobalStringPtr(module.llvm_backend.builder,
                            Literal.ToString(), "SimpleIR_String");
                    return ((SimpleType)Literal).Emit(module);
                }
                case DataTypeKind.Null:
                {
                    return LLVM.ConstNull(LLVMTypeRef.Int1Type());
                }
            }

            return null;
        }
    }
}