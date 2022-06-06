using SimpleIR.LLVMBackend;

namespace SimpleIR
{
    public class Module
    {
        public IR IR;
        public SimpleLLVM llvm_backend;
        public string Name;

        public Module(string name, string target_triple = "i686-pc-windows-gnu")
        {
            Name = name;
            IR = new IR(this);

            llvm_backend = new SimpleLLVM();
            llvm_backend.Initialize(name, target_triple);
        }

        public void Finish()
        {
            llvm_backend.Finish();
        }
    }
}