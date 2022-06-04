using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleIR.LLVMBackend;

namespace SimpleIR
{
    class Module
    {
        public string Name;
        public IR IR;
        public SimpleLLVM llvm_backend;
        public Module(string name)
        {
            this.Name = name;
            IR = new IR(this);

            llvm_backend = new SimpleLLVM();
            llvm_backend.Initialize(name);
        }

        public void Finish()
        {
            llvm_backend.Finish();
        }
    }
}
