using System.Windows.Media;


namespace SimpleLangInterpreter.ColorStyle;

public class CodePainter
{
    public static Color getColor(SyntaxToken token)
    {
        switch (token.TokenType)
        {
            case TokenType.Comment:
            {
                return Colors.Green;
            }
            case TokenType.BinaryToken:
            {
                return Colors.Blue;
            }
            case TokenType.NumberToken:
            {
                return Colors.Pink;
            }
            case TokenType.StringToken:
            {
                return Colors.Orange;
            }
            case TokenType.TypeToken:
            {
                return Colors.DarkBlue;
            }
            case TokenType.Undefined:
            {
                return Colors.Green;
            }
        }
        
        return Colors.Black;
    }
}