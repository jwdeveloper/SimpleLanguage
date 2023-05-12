namespace SL.Parser.Models.Literals;

public class BoolLiteral : Literal
{
    public BoolLiteral(bool value) : base(value, LiteralType.Bool)
    {
    }
}