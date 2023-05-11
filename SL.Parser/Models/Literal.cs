using System.Dynamic;

namespace SL.Parser.Parsing.AST.Expressions;

public abstract class Literal : Expression
{
    public LiteralType LiteralType { get; }

    public object Value { get; }

    public Literal(object value, LiteralType literalType)
    {
        Value = value;
        LiteralType = literalType;
    }


    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.value = Value;
        model.type = LiteralType;
        return model;
    }
}


public enum LiteralType
{
    String,Number,Bool,Identifier
}