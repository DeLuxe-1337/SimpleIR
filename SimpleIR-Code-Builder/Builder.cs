using SimpleIR_Code_Builder.IR_Types;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleIR_Code_Builder
{
    internal class Builder
    {
        public StringBuilder build = new StringBuilder();
        public List<Type> types = new List<Type>();

        public Variable CreateVariable(string name, object data, DataType.DataTypeKind kind = DataType.DataTypeKind.Void)
        {
            var v = new Variable(name, data, kind);
            types.Add(v);

            return v;
        }

        public Function CreateFunction(string name, DataType.DataTypeKind returnKind,
            (string, DataType.DataTypeKind)[] args = null)
        {
            return new Function(name, returnKind, args);
        }

        public Block CreateBlock(string name)
        {
            return new Block(name);
        }

        public static string ConvertData(object data)
        {
            if (data is Type)
                return ((Type)data).ExpressionString();

            if (data is DataType.DataTypeKind)
                return DataType.DataTypeKindToString((DataType.DataTypeKind)data);

            if (data is string)
                return Regex.Escape($"\"{data}\"");

            return data.ToString();
        }
    }
}