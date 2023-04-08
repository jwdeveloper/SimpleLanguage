namespace SL.Core.Parsing.AST.Expressions;

public class ValueHolder : Expression
{
    private readonly object _value;
    private readonly  ValueType _valueType;

    public ValueType ValueType => _valueType;

    public ValueHolder(object value, ValueType valueType)
    {
        _value = value;
        _valueType = valueType;
    }
    
    public override object Execute()
    {
        return _value;
    }
}


public enum ValueType
{
    String,Number,Bool,Reference
}