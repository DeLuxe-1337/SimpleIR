using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIR.SimpleTypes
{
    interface SimpleStatementType
    {
        object Emit(Module module);
    }
    interface SimpleType
    {
        object Emit(Module module);
    }
}
