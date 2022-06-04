using System;
using System.Diagnostics;
using LLVMSharp;

namespace SimpleIR.LLVMBackend
{
    internal class SimpleLLVM
    {
        public LLVMBuilderRef builder;
        public LLVMModuleRef module;

        public void Initialize(string name, string target_triple)
        {
            module = LLVM.ModuleCreateWithName(name);
            builder = LLVM.CreateBuilder();

            var passManager = LLVM.CreateFunctionPassManagerForModule(module);

            LLVM.AddInstructionCombiningPass(passManager);
            LLVM.AddReassociatePass(passManager);
            LLVM.AddGVNPass(passManager);
            LLVM.AddCFGSimplificationPass(passManager);
            LLVM.InitializeFunctionPassManager(passManager);

            LLVM.LinkInMCJIT();
            LLVM.InitializeX86TargetInfo();
            LLVM.InitializeX86Target();
            LLVM.InitializeX86TargetMC();

            LLVM.InitializeX86AsmParser();
            LLVM.InitializeX86AsmPrinter();

            LLVM.SetTarget(module, target_triple);

            var options = new LLVMMCJITCompilerOptions { NoFramePointerElim = 1 };
            LLVM.InitializeMCJITCompilerOptions(options);
            if (LLVM.CreateExecutionEngineForModule(out var engine, module, out var errorMessage).Value == 1)
            {
                Console.WriteLine(errorMessage);
            }
        }

        public void Finish()
        {
            var err = string.Empty;
            LLVM.VerifyModule(module, LLVMVerifierFailureAction.LLVMPrintMessageAction, out err);

            LLVM.DumpModule(module);

            //Write to LL file
            LLVM.PrintModuleToFile(module, "simple_ir_output.ll", out err);
        }

        public void CompileWithWSL()
        {
            Process.Start("WSL-COMPILE.exe");
        }
    }
}