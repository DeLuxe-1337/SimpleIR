using SimpleIR;
using SimpleIR.SimpleTypes;
using SimpleIR.SimpleTypes.Expression;
using SimpleIR.SimpleTypes.Statement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleIR_Code
{
    internal class IR_Compiler : SimpleIRBaseVisitor<object>
    {
        private readonly Stack<Block> blockStack = new Stack<Block>();
        private readonly Dictionary<string, object> env = new Dictionary<string, object>();
        public bool Error;
        private readonly Stack<Function> functionStack = new Stack<Function>();
        public IR IR;
        public Module module = new Module("SimpleIR");

        public IR_Compiler()
        {
            IR = module.IR;
        }

        private void CompileError(string result, string message, int where, int error_length)
        {
            var where_str = new string(' ', where);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("COMPILE ERROR!!\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{result}");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{where_str}{new string('^', error_length)}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{where_str}{message}\n");

            Console.ResetColor();

            Error = true;
        }

        private void CheckForHeaders()
        {
            if (env.ContainsKey("module"))
            {
                module = new Module(((Value)env["module"]).Literal.ToString());
                IR = module.IR;
            }
        }

        public override object VisitConstant(SimpleIRParser.ConstantContext context)
        {
            if (context.STRING() != null)
                return IR.CreateValue(context.STRING().GetText().TrimEnd('"').TrimStart('"'), DataTypeKind.String);

            if (context.NUMBER() != null)
                return IR.CreateValue(int.Parse(context.NUMBER().GetText()), DataTypeKind.Int32);

            if (context.BOOLEAN() != null)
                return IR.CreateValue(bool.Parse(context.BOOLEAN().GetText()), DataTypeKind.Boolean);

            if (context.TYPE() != null)
            {
                var names = Enum.GetNames(typeof(DataTypeKind));
                var values = Enum.GetValues(typeof(DataTypeKind));

                var tName = context.TYPE().GetText();

                foreach (var name in names)
                    if (name.ToLower() == tName)
                        return values.GetValue(names.ToList().IndexOf(name));
            }

            return IR.CreateValue(null, DataTypeKind.Null);
        }

        public override object VisitAssignment(SimpleIRParser.AssignmentContext context)
        {
            var name = context.IDENTIFIER().GetText();
            var assignTo = Visit(context.expression());

            env[name] = assignTo;

            CheckForHeaders();
            return null;
        }

        public override object VisitAssign_with_type(SimpleIRParser.Assign_with_typeContext context)
        {
            var t = (DataTypeKind)Visit(context.expression(0));
            var name = context.IDENTIFIER().GetText();
            var assignTo = Visit(context.expression(1));

            if (DataType.Compare(t, ((Value)assignTo).Kind))
            {
                env[name] = assignTo;
            }
            else
            {
                var typ_str = context.expression(0).GetText();
                var assign_str = context.expression(1).GetText();
                CompileError($"{typ_str} {name} = {assign_str}",
                    $"Try changing this type to {((Value)assignTo).Kind.ToString().ToLower()}.", 0, typ_str.Length);
            }

            return null;
        }

        public override object VisitParam(SimpleIRParser.ParamContext context)
        {
            return IR.GetDataType((DataTypeKind)Visit(context.expression()));
        }

        public override object VisitParams(SimpleIRParser.ParamsContext context)
        {
            var param = new List<SimpleType>();

            foreach (var paramContext in context.param()) param.Add((SimpleType)Visit(paramContext));

            return param.ToArray();
        }

        public override object VisitFunctionDeclare(SimpleIRParser.FunctionDeclareContext context)
        {
            var name = context.function().IDENTIFIER().ToString();
            var type = (DataTypeKind)Visit(context.function().expression());

            SimpleType[] param = { };
            if (context.function().@params() != null)
                param = (SimpleType[])Visit(context.function().@params());

            var f = IR.CreateFunction(name, IR.GetDataType(type), param);

            env.Add(name, f);

            return null;
        }

        public override object VisitFunctionDefine(SimpleIRParser.FunctionDefineContext context)
        {
            var name = context.function().IDENTIFIER().ToString();
            var type = (DataTypeKind)Visit(context.function().expression());

            SimpleType[] param = { };
            if (context.function().@params() != null)
                param = (SimpleType[])Visit(context.function().@params());

            var f = IR.CreateFunction(name, IR.GetDataType(type), param);
            functionStack.Push(f);

            Visit(context.block());

            env.Add(name, f);

            return null;
        }

        public override object VisitBlockDec(SimpleIRParser.BlockDecContext context)
        {
            var name = context.IDENTIFIER().GetText();
            var block = context.block();
            var f = functionStack.Peek();

            var simple_block = IR.CreateBlock(name);
            f.InsertBlock(simple_block);

            blockStack.Push(simple_block);

            Visit(block);

            return null;
        }

        public override object VisitIdentifierExpression(SimpleIRParser.IdentifierExpressionContext context)
        {
            var req = context.IDENTIFIER().GetText();
            return env[req];
        }

        public override object VisitReturn(SimpleIRParser.ReturnContext context)
        {
            var block = blockStack.Peek();
            var f = functionStack.Peek();

            if (context.expression() == null)
            {
                block.CreateReturn();
            }
            else
            {
                var retType = (DataType)f.ReturnType;
                var retValue = (SimpleType)Visit(context.expression());

                if (retValue is Value)
                {
                    var value = (Value)retValue;

                    if (!DataType.Compare(retType.Kind, value.Kind))
                    {
                        var function_build = $"-> function {retType.Kind.ToString().ToLower()} {f.Name}()\n";

                        var ret_build = "return ";
                        var ret_val_text = context.expression().GetText();
                        CompileError($"{function_build}{ret_build}{ret_val_text}",
                            $"Try changing this type to {retType.Kind.ToString().ToLower()}.", ret_build.Length,
                            ret_val_text.Length);
                    }
                }

                block.CreateReturn(retValue);
            }

            return null;
        }

        public override object VisitCallArgs(SimpleIRParser.CallArgsContext context)
        {
            var args = new List<SimpleType>();

            foreach (var expressionContext in context.expression()) args.Add((SimpleType)Visit(expressionContext));

            return args;
        }

        public override object VisitCall(SimpleIRParser.CallContext context)
        {
            var target = Visit(context.expression(0));
            var type = Visit(context.expression(1));
            var args = new List<SimpleType>();

            if (context.callArgs() != null)
                args = (List<SimpleType>)Visit(context.callArgs());

            var block = blockStack.Peek();

            return block.CreateCall((Function)target, args);
        }
    }
}