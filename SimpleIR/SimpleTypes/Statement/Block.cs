using System.Collections.Generic;
using LLVMSharp;
using SimpleIR.SimpleTypes.Expression;

namespace SimpleIR.SimpleTypes.Statement
{
    internal class Block : SimpleStatementType
    {
        private readonly Module module;
        public LLVMValueRef _block;
        public LLVMValueRef _ParentFunction;
        public string Name;

        public Block(string name, Module module)
        {
            Name = name;
            this.module = module;
        }

        public object Emit(Module module)
        {
            var block = LLVM.AppendBasicBlock(_ParentFunction, Name);
            LLVM.PositionBuilderAtEnd(module.llvm_backend.builder, block);

            _block = block;

            return block;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void CreateReturn(SimpleType type = null)
        {
            if (type != null)
                LLVM.BuildRet(module.llvm_backend.builder, (LLVMValueRef)type.Emit(module));
            else
                LLVM.BuildRetVoid(module.llvm_backend.builder);
        }

        public Call CreateCall(Function function, List<SimpleType> arguments)
        {
            var args = new List<LLVMValueRef>();
            foreach (var argument in arguments) args.Add((LLVMValueRef)argument.Emit(module));

            var result = LLVM.BuildCall(module.llvm_backend.builder, function.function, args.ToArray(),
                "call_to_" + function.Name);
            return new Call(result);
        }
    }
}