using SimpleIR_Code_Builder.IR_Types;
using System;

namespace SimpleIR_Code_Builder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var module = new Module("Hello");
            var IR = module.builder;

            IR.CreateVariable("myString", "Hello, world!", DataType.DataTypeKind.String);

            var gets = IR.CreateFunction("gets", DataType.DataTypeKind.String).Finish(IR);
            var printf = IR.CreateFunction("printf", DataType.DataTypeKind.Int32, new (string, DataType.DataTypeKind)[]
            {
                ("input", DataType.DataTypeKind.String)
            }).Finish(IR);

            var f = IR.CreateFunction("main", DataType.DataTypeKind.Void);

            var f_invoke = IR.CreateBlock("invoke");
            f.InsertBlock(f_invoke);

            f_invoke.CreateCall(printf, new object[] { "Hello, world!\nThis is a newline test!" });
            f_invoke.CreateCall(gets);

            f_invoke.CreateReturn();

            f.Finish(IR);

            module.Build();

            Console.ReadLine();
        }
    }
}