namespace SL.Core.Parsing.AST.Expressions;

public abstract class Literal : Expression
{
    public LiteralType LiteralType { get; }

    public object Value { get; }

    public Literal(object value, LiteralType literalType)
    {
        Value = value;
        LiteralType = literalType;
    }
}


public enum LiteralType
{
    String,Number,Bool,Identifier
}