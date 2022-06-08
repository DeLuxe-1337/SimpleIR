using Antlr4.Runtime;
using System;
using System.IO;

namespace SimpleIR_Code
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = File.ReadAllText("source.ir");

            var inputStream = new AntlrInputStream(input);
            var lexer = new SimpleIRLexer(inputStream);
            var CTS = new CommonTokenStream(lexer);
            var parser = new SimpleIRParser(CTS);
            var context = parser.program();
            var visitor = new IR_Compiler();

            visitor.Visit(context);

            if (visitor.Error)
                Console.WriteLine("There's error(s) please fix the errors.");
            else
                visitor.module.Finish();


            Console.ReadLine();
        }
    }
}
