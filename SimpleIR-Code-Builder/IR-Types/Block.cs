using System;
using System.Text;

namespace SimpleIR_Code_Builder.IR_Types
{
    internal class Block : Type
    {
        public string Name;
        public StringBuilder sb = new StringBuilder();

        public Block(string name)
        {
            Name = name;
        }

        public string ExpressionString()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{Indention.ToString()}block {Name} {{\n{sb}{Indention.ToString()}}}";
        }

        public void CreateCall(Function function, object[] args = null)
        {
            Indention.Inc();
            Indention.Inc();

            if (args == null)
            {
                sb.AppendLine(
                    $"{Indention.ToString()}call [{function.Name}, {DataType.DataTypeKindToString(function.Kind)}]");
            }
            else
            {
                var arg = new StringBuilder();
                foreach (var type in args) arg.Append(Builder.ConvertData(type) + ",");

                sb.AppendLine(
                    $"{Indention.ToString()}call [{function.Name}, {DataType.DataTypeKindToString(function.Kind)}, {{{arg.ToString().TrimEnd(',')}}}]");
            }

            Indention.Dec();
            Indention.Dec();
        }

        public void CreateReturn(object data = null)
        {
            Indention.Inc();
            Indention.Inc();

            if (data != null)
                sb.AppendLine($"{Indention.ToString()}return {Builder.ConvertData(data)}");
            else
                sb.AppendLine($"{Indention.ToString()}return");

            Indention.Dec();
            Indention.Dec();
        }
    }
}