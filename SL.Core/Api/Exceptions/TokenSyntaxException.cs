using SL.Core.Common;

namespace SL.Core.Api.Exceptions;

public class BadTokenException : Exception
{

    public BadTokenException(string excepted, Token current) : base(ParseMessage(excepted,current))
    {
        
    }


    private static string ParseMessage(string excepted, Token current)
    {
        return $"Excepted token: {excepted} instead of {current.Value} at position {current.Position}";
    }
}