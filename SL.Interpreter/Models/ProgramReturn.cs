namespace SL.Interpreter.Models;

public class ProgramReturn
{
    public object? Value { get; }
    public string ValueType { get; }

    public ProgramReturn(object? value, string valueType = "")
    {
        Value = value;
        ValueType = valueType;
    }

    public bool HasValue => Value != null;
}