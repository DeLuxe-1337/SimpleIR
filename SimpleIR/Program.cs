using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleIR.SimpleTypes;

namespace SimpleIR
{
    class Program
    {
        static void Main(string[] args)
        {
            Module module = new Module("HelloWorld");
            var IR = module.IR;

            var main = IR.CreateFunction("main", IR.GetDataType(DataTypeKind.Void));
            var printf = IR.CreateFunction("printf", IR.GetDataType(DataTypeKind.Number), IR.GetDataType(DataTypeKind.String));

            var invoke = IR.CreateBlock("invoke");
            main.InsertBlock(invoke);

            invoke.CreateCall(printf, new List<SimpleType>()
            {
                IR.CreateValue("Hello, world\n", DataTypeKind.String)
            });

            invoke.CreateReturn();

            module.Finish();
            module.llvm_backend.CompileWithWSL();

            Console.ReadKey();
        }
    }
}
