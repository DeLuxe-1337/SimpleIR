namespace SimpleIR.SimpleTypes
{
    public interface SimpleStatementType
    {
        object Emit(Module module);
    }

    public interface SimpleType
    {
        object Emit(Module module);
    }
}