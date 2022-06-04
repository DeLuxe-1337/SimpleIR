using LLVMSharp;

namespace SimpleIR.SimpleTypes.Expression
{
    internal class Value : SimpleType
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
            }

            return null;
        }
    }
}