using SL.Parser.Common;

namespace SL.Parser.Api.Exceptions;

public class SyntaxException : Exception
{
    public SyntaxException(string message, Token token) : base(ParseMessage(message,token))
    {
        
    }
    
    private static string ParseMessage(string message, Token token)
    {
        return $"{message} given value {token} at position {token.Position}";
    }
}