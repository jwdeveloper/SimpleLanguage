namespace SimpleLangInterpreter.Core;

public record DefaultToken
{
    public string Name { get; set; }
    public char Symbol { get; set; }
    
    public TokenType TokenType { get; set; }

    public static List<DefaultToken> GetDefaultTokens()
    {
        return new List<DefaultToken>()
        {
            COMMA,
            OPEN_FUNCTION,
            OPEN_OPERATION,
            CLOSE_FUNCTION,
            CLOSE_OPERATION,
            END_LINE,
            LESS,
            GREATER,
            EQUAL,
            PLUS,
            MINUS,
            TIMES,
            DIVIDE,
            POWER,
            QUOTE,
            DOT,
            SINGLE_AND,
            SINGLE_OR
        };
    }
    
    
    public static DefaultToken COMMA = new()
    {
        Name = "COMMA",
        Symbol = ',',
        TokenType = TokenType.Undefined
    };
    
    public static DefaultToken OPEN_FUNCTION = new()
    {
        Name = "OPEN_FUNCTION",
        Symbol = '(',
        TokenType = TokenType.Undefined
    };
    
    public static DefaultToken CLOSE_FUNCTION = new()
    {
        Name = "CLOSE_FUNCTION",
        Symbol = ')',
        TokenType = TokenType.Undefined
    };
    
    public static DefaultToken OPEN_OPERATION = new()
    {
        Name = "OPEN_OPERATION",
        Symbol = '{',
        TokenType = TokenType.Undefined
    };
    
    public static DefaultToken CLOSE_OPERATION = new()
    {
        Name = "CLOSE_OPERATION",
        Symbol = '}',
        TokenType = TokenType.Undefined
    };
    
    public static DefaultToken END_LINE = new()
    {
        Name = "END_LINE",
        Symbol = ';',
        TokenType = TokenType.Undefined
    };
    
    public static DefaultToken LESS = new()
    {
        Name = "LESS",
        Symbol = '<',
        TokenType = TokenType.BinaryToken
    };
    
    public static DefaultToken GREATER = new()
    {
        Name = "GREATER",
        Symbol = '>',
        TokenType = TokenType.BinaryToken
    };
    
    public static DefaultToken EQUAL = new()
    {
        Name = "EQUAL",
        Symbol = '=',
        TokenType = TokenType.BinaryToken
    };
    
    public static DefaultToken PLUS = new()
    {
        Name = "PLUS",
        Symbol = '+',
        TokenType = TokenType.BinaryToken
    };
    
    public static DefaultToken MINUS = new()
    {
        Name = "MINUS",
        Symbol = '-',
        TokenType = TokenType.BinaryToken
    };
    
    public static DefaultToken TIMES = new()
    {
        Name = "TIMES",
        Symbol = '*',
        TokenType = TokenType.BinaryToken
    };
    
    public static DefaultToken DIVIDE = new()
    {
        Name = "DIVIDE",
        Symbol = '/',
        TokenType = TokenType.BinaryToken
    };
    
    public static DefaultToken POWER = new()
    {
        Name = "POWER",
        Symbol = '^',
        TokenType = TokenType.BinaryToken
    };
    
    public static DefaultToken QUOTE = new()
    {
        Name = "QUOTE",
        Symbol = '"',
        TokenType = TokenType.Undefined
    };
    
    public static DefaultToken DOT = new()
    {
        Name = "DOT",
        Symbol = '.',
        TokenType = TokenType.Undefined
    };
    
    public static DefaultToken SINGLE_AND = new()
    {
        Name = "SINGLE_AND",
        Symbol = '&',
        TokenType = TokenType.Undefined
    };
    
    public static DefaultToken SINGLE_OR = new()
    {
        Name = "SINGLE_OR",
        Symbol = '|',
        TokenType = TokenType.Undefined
    };
}