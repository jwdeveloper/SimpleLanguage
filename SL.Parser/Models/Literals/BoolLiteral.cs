namespace SL.Parser.Parsing.AST.Expressions;

public class BoolLiteral : Literal
{
    public BoolLiteral(bool value) : base(value, LiteralType.Bool)
    {
    }
}