namespace SL.Parser.Models.Literals;

public class NumericLiteral : Literal
{
    public NumericLiteral(float value) : base(value, LiteralType.Number)
    {
        
    }
}