using System.Collections.Generic;
using LLVMSharp;

namespace SimpleIR.SimpleTypes.Statement
{
    class Block : SimpleStatementType
    {
        public string Name;
        public LLVMValueRef _ParentFunction;
        private Module module;
        public LLVMValueRef _block;
        public Block(string name, Module module)
        {
            this.Name = name;
            this.module = module;
        }
        public void SetName(string name)
        {
            this.Name = name;
        }

        public object Emit(Module module)
        {
            var block = LLVM.AppendBasicBlock(_ParentFunction, this.Name);
            LLVM.PositionBuilderAtEnd(module.llvm_backend.builder, block);

            this._block = block;

            return block;
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
            foreach (var argument in arguments)
            {
                args.Add((LLVMValueRef)argument.Emit(module));
            }

            var result = LLVM.BuildCall(module.llvm_backend.builder, function.function, args.ToArray(), "call_to_" + function.Name);
            return new Call(result);
        }
    }
}
