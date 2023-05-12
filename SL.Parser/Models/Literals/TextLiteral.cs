namespace SL.Parser.Models.Literals;

public class TextLiteral : Literal
{
    public TextLiteral(string value) : base(value, LiteralType.String)
    {
        
    }
}