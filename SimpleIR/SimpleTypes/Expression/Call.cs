using LLVMSharp;

namespace SimpleIR.SimpleTypes.Expression
{
    public class Call : SimpleType
    {
        public LLVMValueRef llvm_result;

        public Call(LLVMValueRef result)
        {
            llvm_result = result;
        }

        public object Emit(Module module)
        {
            return llvm_result;
        }
    }
}