namespace SL.Parser.Parsing.AST.Expressions;

public class TextLiteral : Literal
{
    public TextLiteral(string value) : base(value, LiteralType.String)
    {
        
    }
}