namespace SimpleIR_Code_Builder.IR_Types
{
    internal class DataType
    {
        public enum DataTypeKind
        {
            Void,
            Number,
            String,
            Boolean,
            Null,

            Int32,
            Float,
            FP128,
            Int16,
            Int64,
            Int8,
            Int1,
            IntPtr
        }

        public static string DataTypeKindToString(DataTypeKind kind)
        {
            return kind.ToString().ToLower();
        }

        public static bool Compare(DataTypeKind left, DataTypeKind right)
        {
            if ((left == DataTypeKind.Int32 || right == DataTypeKind.Int32) &&
                (left == DataTypeKind.Number || right == DataTypeKind.Number))
                return true;

            return left == right;
        }

        public static DataTypeKind GetKindFromType(object value)
        {
            var type = value.GetType();

            if (type == typeof(string))
                return DataTypeKind.String;
            if (type == typeof(bool))
                return DataTypeKind.Boolean;
            if (type == typeof(int))
                return DataTypeKind.Int32;
            if (type == typeof(long))
                return DataTypeKind.Int64;
            if (type == typeof(short))
                return DataTypeKind.Int16;
            if (type == typeof(int))
                return DataTypeKind.Int32;
            if (type == typeof(float))
                return DataTypeKind.Float;

            return DataTypeKind.Null;
        }
    }
}