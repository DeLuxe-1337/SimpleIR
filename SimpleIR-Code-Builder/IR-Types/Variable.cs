namespace SimpleIR_Code_Builder.IR_Types
{
    internal class Variable : Type
    {
        public DataType.DataTypeKind Kind;
        public string Name;
        public object Value;

        public Variable(string name, object value, DataType.DataTypeKind Kind = DataType.DataTypeKind.Void)
        {
            Name = name;
            Value = value;
            this.Kind = Kind;
        }

        public string ExpressionString()
        {
            return Name;
        }

        public override string ToString()
        {
            if (Kind != DataType.DataTypeKind.Void)
                return $"{DataType.DataTypeKindToString(Kind)} {Name} = {Builder.ConvertData(Value)}";
            return $"{Name} = {Builder.ConvertData(Value)}";
        }
    }
}