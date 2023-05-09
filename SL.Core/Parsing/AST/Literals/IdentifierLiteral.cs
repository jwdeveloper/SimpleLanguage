namespace SL.Core.Parsing.AST.Expressions;

public class IdentifierLiteral : Literal
{
    public string IdentifierName => (string)Value;
    public IdentifierLiteral(string value) : base(value, LiteralType.Identifier)
    {
    }
}