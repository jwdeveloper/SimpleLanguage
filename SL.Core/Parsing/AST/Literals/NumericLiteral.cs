namespace SL.Core.Parsing.AST.Expressions;

public class NumericLiteral : Literal
{
    public NumericLiteral(float value) : base(value, LiteralType.Number)
    {
        
    }
}