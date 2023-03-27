using SimpleLangInterpreter.Core;

namespace SimpleLangInterpreter;

public record SyntaxToken
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    
    public Position Position { get; set; }

    public TokenType TokenType { get; set; } = TokenType.Undefined;

    public override string ToString()
    {
        return $"{Name} -> {Symbol}  \n";
    }

  
    
    public static SyntaxToken COMMA = new()
    {
        Name = "COMMA",
        Symbol = ","
    };
    
    public static SyntaxToken OPEN_FUNCTION = new()
    {
        Name = "OPEN_FUNCTION",
        Symbol = "("
    };
    
    public static SyntaxToken CLOSE_FUNCTION = new()
    {
        Name = "CLOSE_FUNCTION",
        Symbol = ")"
    };
    
    public static SyntaxToken OPEN_OPERATION = new()
    {
        Name = "OPEN_OPERATION",
        Symbol = "{"
    };
    
    public static SyntaxToken CLOSE_OPERATION = new()
    {
        Name = "CLOSE_OPERATION",
        Symbol = "}"
    };
    
    public static SyntaxToken END_LINE = new()
    {
        Name = "End line",
        Symbol = ";"
    };
    
    public static SyntaxToken LESS = new()
    {
        Name = "LESS",
        Symbol = "<"
    };
    
    public static SyntaxToken MORE = new()
    {
        Name = "MORE",
        Symbol = ">"
    };
    
    public static SyntaxToken EQUAL = new()
    {
        Name = "Equal",
        Symbol = "="
    };
    
    public static SyntaxToken PLUS = new()
    {
        Name = "PLUS",
        Symbol = "+"
    };
    
    public static SyntaxToken MINUS = new()
    {
        Name = "MINUS",
        Symbol = "-"
    };
    
    public static SyntaxToken TIMES = new()
    {
        Name = "Times",
        Symbol = "*"
    };
    
    public static SyntaxToken DIVIDE = new()
    {
        Name = "DIVIDE",
        Symbol = "/"
    };
    
    public static SyntaxToken TEXT = new()
    {
        Name = "TEXT",
        Symbol = "\""
    };
    
    public static SyntaxToken UNKNOWN = new()
    {
        Name = "UNKNOWN",
        Symbol = ""
    };
    
    public static SyntaxToken AND = new()
    {
        Name = "AND",
        Symbol = "and"
    };
    
    public static SyntaxToken OR = new()
    {
        Name = "OR",
        Symbol = "or"
    };
    
    public static SyntaxToken IF = new()
    {
        Name = "IF",
        Symbol = "if"
    };
    
    public static SyntaxToken FOR = new()
    {
        Name = "FOR",
        Symbol = "for"
    };
    
    public static SyntaxToken WHILE = new()
    {
        Name = "WHILE",
        Symbol = "while"
    };


    public static SyntaxToken END_OF_FILE()
    {
        return new SyntaxToken()
        {
            Symbol = "END_OF_FILE",
            Name = "END_OF_FILE",
            TokenType = TokenType.EndOfFile
        };
    }
}