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

        public static bool Compare(DataTypeKind left, DataTypeKind right)
        {
            if ((left == DataTypeKind.Int32 || right == DataTypeKind.Int32) &&
                (left == DataTypeKind.Number || right == DataTypeKind.Number))
                return true;

            return left == right;
        }
        public static DataTypeKind GetKindFromType(object value)
        {
            var type = value.GetType();

            if (type == typeof(string))
                return DataTypeKind.String;
            if (type == typeof(bool))
                return DataTypeKind.Boolean;
            if (type == typeof(int))
                return DataTypeKind.Int32;
            if (type == typeof(long))
                return DataTypeKind.Int64;
            if (type == typeof(short))
                return DataTypeKind.Int16;
            if (type == typeof(int))
                return DataTypeKind.Int32;
            if (type == typeof(float))
                return DataTypeKind.Float;

            return DataTypeKind.Null;
        }
    }
}