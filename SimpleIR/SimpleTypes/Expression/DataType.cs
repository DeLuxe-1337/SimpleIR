using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using LLVMSharp;

namespace SimpleIR.SimpleTypes
{
    enum DataTypeKind
    {
        Void, Number, String
    }
    class DataType : SimpleType
    {
        public DataTypeKind Kind;
        public object Emit(Module module)
        {
            switch (Kind)
            {
                case DataTypeKind.Number:
                {
                    return LLVM.Int32Type();
                }
                case DataTypeKind.Void:
                {
                    return LLVM.VoidType();
                }
                case DataTypeKind.String:
                {
                    return LLVM.PointerType(LLVMTypeRef.Int8Type(), 0);
                }
            }

            return null;
        }

        public DataType(DataTypeKind kind)
        {
            this.Kind = kind;
        }
    }
}
