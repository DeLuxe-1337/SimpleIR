using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLVMSharp;

namespace SimpleIR.SimpleTypes
{
    class Call : SimpleType
    {
        public LLVMValueRef llvm_result;

        public Call(LLVMValueRef result)
        {
            this.llvm_result = result;
        }
        public object Emit(Module module)
        {
            return llvm_result;
        }
    }
}
