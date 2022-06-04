namespace SimpleIR.SimpleTypes
{
    internal interface SimpleStatementType
    {
        object Emit(Module module);
    }

    internal interface SimpleType
    {
        object Emit(Module module);
    }
}