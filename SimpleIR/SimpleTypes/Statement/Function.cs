using System.Collections.Generic;
using LLVMSharp;

namespace SimpleIR.SimpleTypes.Statement
{
    class Function : SimpleStatementType
    {
        public string Name;
        public List<SimpleType> Arguments;
        public SimpleType ReturnType;
        public LLVMValueRef function;
        private Module module;
        private List<Block> Body = new List<Block>();

        public Function(string name, SimpleType returnType, Module module, List<SimpleType> args = null)
        {
            this.Name = name;
            this.Arguments = args;
            this.ReturnType = returnType;

            this.module = module;

            this.Emit(module);
        }

        public object Emit(Module module)
        {
            List<LLVMTypeRef> argument_kind = new List<LLVMTypeRef>();
            if (Arguments != null)
            {
                foreach (var simpleType in Arguments)
                {
                    argument_kind.Add((LLVMTypeRef)simpleType.Emit(module));
                }
            }

            var returnType = (LLVMTypeRef)ReturnType.Emit(module);

            var fn = LLVM.AddFunction(module.llvm_backend.module, this.Name,
                LLVM.FunctionType(returnType, argument_kind.ToArray(), false));
            function = fn;

            return null;
        }

        public void InsertBlock(Block body)
        {
            body._ParentFunction = function;
            body.Emit(module);
            this.Body.Add(body);
        }

        public void SetName(string name)
        {
            this.Name = name;
        }
    }
}
