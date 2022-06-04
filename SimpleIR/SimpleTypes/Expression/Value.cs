using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLVMSharp;

namespace SimpleIR.SimpleTypes
{
    class Value : SimpleType
    {
        public DataTypeKind Kind;
        public object Literal;

        public Value(object literal, DataTypeKind kind)
        {
            this.Literal = literal;
            this.Kind = kind;
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
                    else
                        return ((SimpleType)Literal).Emit(module);
                }
            }

            return null;
        }
    }
}
