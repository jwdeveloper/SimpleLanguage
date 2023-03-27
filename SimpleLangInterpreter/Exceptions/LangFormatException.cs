using SimpleLangInterpreter.Core;

namespace SimpleLangInterpreter.Exceptions;

public class LangFormatException : Exception
{
    public LangFormatException(string message, Position position) : base(message+" "+position)
    {
 
        
    }
    
    public LangFormatException(string message) : base(message)
    {
 
        
    }
}