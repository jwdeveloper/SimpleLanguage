namespace SL.Core.Api.Exceptions;

public class SyntaxException : Exception
{
    public SyntaxException(string message, IPosition position) : base(ParseMessage(message,position))
    {
        
    }
    
    private static string ParseMessage(string message, IPosition position)
    {
        return $"{message} at position {position}";
    }
}