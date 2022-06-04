using System.Collections.Generic;
using LLVMSharp;

namespace SimpleIR.SimpleTypes.Statement
{
    internal class Function : SimpleStatementType
    {
        public List<SimpleType> Arguments;
        private readonly List<Block> Body = new List<Block>();
        public LLVMValueRef function;
        private readonly Module module;
        public string Name;
        public SimpleType ReturnType;

        public Function(string name, SimpleType returnType, Module module, List<SimpleType> args = null)
        {
            Name = name;
            Arguments = args;
            ReturnType = returnType;

            this.module = module;

            Emit(module);
        }

        public object Emit(Module module)
        {
            var argument_kind = new List<LLVMTypeRef>();
            if (Arguments != null)
                foreach (var simpleType in Arguments)
                    argument_kind.Add((LLVMTypeRef)simpleType.Emit(module));

            var returnType = (LLVMTypeRef)ReturnType.Emit(module);

            var fn = LLVM.AddFunction(module.llvm_backend.module, Name,
                LLVM.FunctionType(returnType, argument_kind.ToArray(), false));
            function = fn;

            return null;
        }

        public void InsertBlock(Block body)
        {
            body._ParentFunction = function;
            body.Emit(module);
            Body.Add(body);
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}