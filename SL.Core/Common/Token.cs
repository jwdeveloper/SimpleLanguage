using SL.Core.Api;

namespace SL.Core.Common;

public record Token(TokenType Type, string Value, IPosition Position)
{
    public static Token EndOfFile(IPosition position) => new Token(TokenType.END_OF_FILE, "END OF FILE", position);
    
    public static Token BadToken(string message, IPosition position) => new Token(TokenType.BAD_TOKEN, message, position);
    
    public static Token Canceled(IPosition position) => new Token(TokenType.BAD_TOKEN, "Lexing Canceled", position);
  
}

public enum TokenType
{
    END_OF_FILE,
    END_OF_LINE,
    BAD_TOKEN,
    IGNORED,
    SPACE,

    OPEN_BLOCK,
    CLOSE_BLOCK,
    
    OPEN_ARGUMETNS,
    CLOSE_ARGUMENTS,
    COMMA,

    
    //Variable
    STRING,
    NUMBER,
    BOOL,
    IDENTIFIER,
    
  
    ASSIGMENT,
    COMPLEX_ASSIGMENT,
    BINARY_ADDATIVE_OPERATOR,
    BINARY_MULTIPLICATION_OPERATOR,
    EQUALITY_OPREATOR,
    LOGICAL_OPERATOR,
    
    KEYWORLD,
    OBJECT_TYPE,
    DOT
}