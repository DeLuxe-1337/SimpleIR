﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLVMSharp;

namespace SimpleIR.LLVMBackend
{
    class SimpleLLVM
    {
        public LLVMModuleRef module;
        public LLVMBuilderRef builder;

        public void Initialize(string name)
        {
            module = LLVM.ModuleCreateWithName(name);
            builder = LLVM.CreateBuilder();

            LLVMPassManagerRef passManager = LLVM.CreateFunctionPassManagerForModule(module);

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

            LLVMMCJITCompilerOptions options = new LLVMMCJITCompilerOptions { NoFramePointerElim = 1 };
            LLVM.InitializeMCJITCompilerOptions(options);
            if (LLVM.CreateExecutionEngineForModule(out var engine, module, out var errorMessage).Value == 1)
            {
                Console.WriteLine(errorMessage);
                return;
            }
        }
        public void Finish()
        {
            string err = String.Empty;
            LLVM.VerifyModule(module, LLVMVerifierFailureAction.LLVMPrintMessageAction, out err);

            LLVM.DumpModule(module);

            //Write to LL file
            LLVM.PrintModuleToFile(module, "simple_ir_output.ll", out err);

            File.WriteAllText("simple_ir_output.ll",
                "target triple = \"i686-pc-windows-gnu\"\n" + File.ReadAllText("simple_ir_output.ll"));
        }

        public void CompileWithWSL()
        {
            Process.Start("WSL-COMPILE.exe");
        }
    }
}
