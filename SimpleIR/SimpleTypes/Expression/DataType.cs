using LLVMSharp;

namespace SimpleIR.SimpleTypes.Expression
{
    internal enum DataTypeKind
    {
        Void,
        Number,
        String
    }

    internal class DataType : SimpleType
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