using LLVMSharp;
using System;

namespace SimpleIR.SimpleTypes.Expression
{
    public enum DataTypeKind
    {
        Void,
        Number,
        String,
        Boolean,
        Null,

        Int32,
        Float,
        FP128,
        Int16,
        Int64,
        Int8,
        Int1,
        IntPtr
    }

    public class DataType : SimpleType
    {
        public static DataTypeKind GetKindFromType(object value)
        {
            DataTypeKind kind = DataTypeKind.Null;

            var type = value.GetType();

            if (type == typeof(string))
                kind = DataTypeKind.String;
            if (type == typeof(bool))
                kind = DataTypeKind.Boolean;
            if (type == typeof(Int32))
                kind = DataTypeKind.Int32;
            if (type == typeof(Int64))
                kind = DataTypeKind.Int64;
            if (type == typeof(Int16))
                kind = DataTypeKind.Int16;
            if (type == typeof(int))
                kind = DataTypeKind.Int32;
            if (type == typeof(float))
                kind = DataTypeKind.Float;

            return kind;
        }
        public DataTypeKind Kind;

        public DataType(DataTypeKind kind)
        {
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
                        return LLVM.Int64Type();
                    }
                case DataTypeKind.Int16:
                    {
                        return LLVM.Int16Type();
                    }
                case DataTypeKind.Float:
                    {
                        return LLVM.FloatType();
                    }
                case DataTypeKind.FP128:
                    {
                        return LLVM.FP128Type();
                    }
                case DataTypeKind.Int8:
                    {
                        return LLVM.FP128Type();
                    }
                case DataTypeKind.Null:
                case DataTypeKind.Int1:
                case DataTypeKind.Boolean:
                    {
                        return LLVM.Int1Type();
                    }
                case DataTypeKind.Int32:
                case DataTypeKind.Number:
                    {
                        return LLVM.Int32Type();
                    }
                case DataTypeKind.Void:
                    {
                        return LLVM.VoidType();
                    }
                case DataTypeKind.String:
                    {
                        return LLVM.PointerType(LLVMTypeRef.Int8Type(), 0);
                    }
            }

            return null;
        }
    }
}