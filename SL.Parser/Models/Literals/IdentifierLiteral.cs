using System.Dynamic;

namespace SL.Parser.Models.Literals;

public class IdentifierLiteral : Literal
{
    public string IdentifierName => (string)Value;

    public IdentifierLiteral? NextCall;
    public IdentifierLiteral(string value, IdentifierLiteral? nextCall = null) : base(value, LiteralType.Identifier)
    {
        NextCall = nextCall;
    }
    
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.value = Value;
        model.type = LiteralType;
        model.nextCall = NextCall?.GetModel();
        return model;
    }
}