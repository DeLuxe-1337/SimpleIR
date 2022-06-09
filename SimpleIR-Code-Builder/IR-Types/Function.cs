using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIR_Code_Builder.IR_Types
{
    internal class Function : Type
    {
        public List<(string, DataType.DataTypeKind)> args;
        public List<Block> block = new List<Block>();
        public DataType.DataTypeKind Kind;
        public string Name;

        public Function(string name, DataType.DataTypeKind kind, (string, DataType.DataTypeKind)[] args = null)
        {
            Name = name;
            Kind = kind;
            if (args != null)
                this.args = args.ToList();
        }

        public string ExpressionString()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            if (block.Count > 0)
            {
                Indention.Inc();

                var sb = new StringBuilder();
                foreach (var block1 in block) sb.AppendLine(block1.ToString());

                var block_str = $"{{\n{sb}\n}}";
                Indention.Dec();

                var argSb = new StringBuilder();
                if (args != null)
                    foreach (var valueTuple in args)
                        argSb.Append($"{valueTuple.Item1}: {Builder.ConvertData(valueTuple.Item2)}, ");

                return
                    $"function {DataType.DataTypeKindToString(Kind)} {Name}({argSb.ToString().TrimEnd(',', ' ')}) {block_str}";
            }
            else
            {
                var argSb = new StringBuilder();
                if (args != null)
                    foreach (var valueTuple in args)
                        argSb.Append($"{valueTuple.Item1}: {Builder.ConvertData(valueTuple.Item2)}, ");
                return $"function {DataType.DataTypeKindToString(Kind)} {Name}({argSb.ToString().TrimEnd(',', ' ')})";
            }
        }

        public void InsertBlock(Block block)
        {
            this.block.Add(block);
        }

        public Function Finish(Builder builder)
        {
            builder.types.Add(this);

            return this;
        }
    }
}