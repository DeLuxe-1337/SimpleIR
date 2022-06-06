using SimpleIR.SimpleTypes;
using SimpleIR.SimpleTypes.Expression;
using System;
using System.Collections.Generic;

namespace SimpleIR
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var module = new Module("HelloWorld");
            var IR = module.IR;

            var main = IR.CreateFunction("main", IR.GetDataType(DataTypeKind.Void));

            var invoke = IR.CreateBlock("invoke");
            main.InsertBlock(invoke);

            var printf = IR.CreateFunction("printf", IR.GetDataType(DataTypeKind.Number),
                IR.GetDataType(DataTypeKind.String));
            invoke.CreateCall(printf, new List<SimpleType>
            {
                IR.CreateValue("Hello, world\n", DataTypeKind.String)
            });

            //prevent console from closing
            var gets = IR.CreateFunction("gets", IR.GetDataType(DataTypeKind.String));
            invoke.CreateCall(gets, new List<SimpleType>());

            invoke.CreateReturn();

            module.Finish();
            module.llvm_backend.CompileWithWSL();

            Console.ReadKey();
        }
    }
}