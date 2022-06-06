using LLVMSharp;
using SimpleIR.SimpleTypes;
using SimpleIR.SimpleTypes.Expression;
using SimpleIR.SimpleTypes.Statement;
using System.Linq;

namespace SimpleIR
{
    public class IR
    {
        private readonly Module module;

        public IR(Module mod)
        {
            module = mod;
        }

        public void SetPosition(Block position)
        {
            LLVM.PositionBuilderAtEnd(module.llvm_backend.builder, position._block);
        }

        public Function CreateFunction(string name, SimpleType returnType, params SimpleType[] args)
        {
            return new Function(name, returnType, module, args.ToList());
        }

        public DataType GetDataType(DataTypeKind kind)
        {
            return new DataType(kind);
        }

        public Block CreateBlock(string name)
        {
            return new Block(name, module);
        }

        public Value CreateValue(object value, DataTypeKind kind)
        {
            return new Value(value, kind);
        }

        public Value CreateValueAuto(object value)
        {
            return new Value(value, DataType.GetKindFromType(value));
        }
    }
}