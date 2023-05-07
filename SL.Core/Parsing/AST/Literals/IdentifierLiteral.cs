namespace SL.Core.Parsing.AST.Expressions;

public class IdentifierLiteral : Literal
{
    public IdentifierLiteral(string value) : base(value, LiteralType.Identifier)
    {
    }
}