namespace SimpleLangInterpreter.Core;

public class LexerPostProcessor
{
    private readonly List<SyntaxToken> tokens;
    private SyntaxToken currnet;
    private int position;

    public LexerPostProcessor(List<SyntaxToken> tokens)
    {
        this.tokens = tokens;
        this.position = -1;
        this.currnet = null;
    }

    public SyntaxToken Current()
    {
        return currnet;
    }
    
    public SyntaxToken Advance()
    {
        position++;
        if (position >= tokens.Count)
        {
            currnet = SyntaxToken.END_OF_FILE();
            return currnet;
        }
        currnet = tokens[position];
        return currnet;
    }

    public bool IsValid()
    {
        return currnet != null && currnet.TokenType != TokenType.EndOfFile;
    }
    
    private SyntaxToken Peek(int offset)
    {
        var index = position + offset;

        if (index >= tokens.Count)
            return SyntaxToken.END_OF_FILE();

        return tokens[index];
    }
    
    
    
    public List<SyntaxToken> FilterTokens()
    {
        var result = new List<SyntaxToken>();
        Advance();
        while (IsValid())
        {
            var current = Current();
            var next = Peek(1);
            
            // handle + = -> += 
            if (current.TokenType == TokenType.BinaryToken && next.Symbol == "=")
            {
                result.Add(new SyntaxToken()
                {
                    Name = current.Name+" "+next.Name,
                    Position = current.Position,
                    Symbol = current.Symbol+next.Symbol,
                    TokenType = current.TokenType
                });
                Advance();
                Advance();
                continue;
            }

            // handle 123.123 -> float 123.123
            if (current.TokenType == TokenType.NumberToken &&
                Peek(1).Symbol == "." &&
                Peek(2).TokenType == TokenType.NumberToken)
            {
                var peek1 = Peek(1);
                var peek2 = Peek(2);
                
                result.Add(new SyntaxToken()
                {
                    Name = "NUMBER:Float",
                    Position = current.Position,
                    Symbol = current.Symbol+peek1.Symbol+peek2.Symbol,
                    TokenType = TokenType.NumberToken
                });
                Advance();
                Advance();
                Advance();
                continue;
            }
            
            // handle .123 -> float 0.123
            if (current.Symbol ==  "." &&
                Peek(1).TokenType == TokenType.NumberToken)
            {
                var peek1 = Peek(1);
                result.Add(new SyntaxToken()
                {
                    Name = "NUMBER:Float",
                    Position = current.Position,
                    Symbol = "0"+current.Symbol+peek1.Symbol,
                    TokenType = TokenType.NumberToken
                });
                Advance();
                Advance();
                continue;
            }
            
            
            result.Add(current);
            Advance();
        }
        return result;
    }
    
    
    /*
     *    var isOperation = _defaultOperations.Find(c => c.Value == value);
            if (isOperation != null)
            {
                result.Add(new SyntaxToken
                {
                    Name = "OPERATION",
                    Symbol = value
                });
                continue;
            }

            var isType = _defaultTypes.Find(c => c.Value == value);
            if (isType != null)
            {
                result.Add(new SyntaxToken
                {
                    Name = "TYPE:" + isType.Name,
                    Symbol = value
                });
                continue;
            }

            var isKeyWord = _defaultKeywords.Find(c => c.Value == value);
            if (isKeyWord != null)
            {
                result.Add(new SyntaxToken
                {
                    Name = "Keyword",
                    Symbol = value
                });
                continue;
            }
     */

}