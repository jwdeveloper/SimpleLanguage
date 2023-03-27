namespace SimpleLangInterpreter.Core;

public class LexerPostProcessor : TokenIterator
{
    private readonly List<SyntaxToken> tokens;
    private SyntaxToken currnet;
    private int position;

    public LexerPostProcessor(List<SyntaxToken> tokens) : base(tokens)
    {
    }


    
    
    public List<SyntaxToken> FilterTokens()
    {
        var result = new List<SyntaxToken>();
        Advance();
        while (IsValid())
        {
            var current = Current();
            // handle + = -> += 
            if (HandleBinaryExp(out var tokenExp))
            {
                result.Add(tokenExp);
                continue;
            }
            
            // handle .123 -> float 0.123
            if (HandleFloat(out var tokenFloat))
            {
                result.Add(tokenFloat);
                continue;
            }
            
            // handle 123.123 -> float 123.123
            if (HandleFloat2(out var tokenFloat2))
            {
                result.Add(tokenFloat2);
                continue;
            }

            if (HandleType(out var tokenType))
            {
                result.Add(tokenType);
                continue;
            }
            
            if (HandleOperation(out var tokenOperation))
            {
                result.Add(tokenOperation);
                continue;
            }

            if (HandleKeyWord(out var tokenKeyword))
            {
                result.Add(tokenKeyword);
                continue;
            }


            if (HandleLogic(out var logic))
            {
                result.Add(logic);
                continue;
            }
            
            result.Add(current);
            Advance();
        }
        return result;
    }



    private bool HandleBinaryExp(out SyntaxToken token)
    {
        token = null;
        var current = Current();
        var next = Peek(1);
        if (current.TokenType == TokenType.BinaryToken && next.Symbol == "=")
        {
            token = new SyntaxToken()
            {
                Name = current.Name+" "+next.Name,
                Position = current.Position,
                Symbol = current.Symbol+next.Symbol,
                TokenType = current.TokenType
            };
            Advance();
            Advance();
            return true;
        }
        return false;
    }

    private bool HandleFloat(out SyntaxToken token)
    {
        token = null;
        var current = Current();
        if (current.Symbol ==  "." &&
            Peek(1).TokenType == TokenType.NumberToken)
        {
            var peek1 = Peek(1);
            token = new SyntaxToken()
            {
                Name = "NUMBER:Float",
                Position = current.Position,
                Symbol = "0"+current.Symbol+peek1.Symbol,
                TokenType = TokenType.NumberToken
            };
            Advance();
            Advance();
            return true;
        }
        return false;
    }


    private bool HandleFloat2(out SyntaxToken token)
    {
        token = null;
        var current = Current();
        if (current.TokenType == TokenType.NumberToken &&
            Peek(1).Symbol == "." &&
            Peek(2).TokenType == TokenType.NumberToken)
        {
            var peek1 = Peek(1);
            var peek2 = Peek(2);
                
            token = new SyntaxToken()
            {
                Name = "NUMBER:Float",
                Position = current.Position,
                Symbol = current.Symbol+peek1.Symbol+peek2.Symbol,
                TokenType = TokenType.NumberToken
            };
            Advance();
            Advance();
            Advance();
            return true;
        }
        return false;
    }
    
    
    private bool HandleType(out SyntaxToken token)
    {
        token = null;
        var current = Current();
        if (current.TokenType != TokenType.LitteralToken)
        {
            return false;
        }
        var isType = DefaultType.GetDefaultTypes().Find(c => c.Value == current.Symbol);
        if (isType == null)
        {
            return false;
        }
        
        
        token = new SyntaxToken
        {
            Name = "TYPE:" + isType.Name,
            Symbol = isType.Value,
            TokenType = TokenType.TypeToken,
            Position = current.Position
        };
        Advance();
        return true;
    }
    
    
    
    private bool HandleOperation(out SyntaxToken token)
    {
        token = null;
        var current = Current();
        if (current.TokenType != TokenType.LitteralToken)
        {
            return false;
        }
        var isFound = DefaultOperations.GetDefaultOperations().Find(c => c.Value == current.Symbol);
        if (isFound == null)
        {
            return false;
        }
        
        token = new SyntaxToken
        {
            Name = "OPERATION:" + isFound.Name,
            Symbol = isFound.Value,
            TokenType = TokenType.OperationToken,
            Position = current.Position
        };
        Advance();
        return true;
    }
    
    private bool HandleKeyWord(out SyntaxToken token)
    {
        token = null;
        var current = Current();
        if (current.TokenType != TokenType.LitteralToken)
        {
            return false;
        }
        var isFound = DefaultKeywords.GetDefaultKeywords().Find(c => c.Value == current.Symbol);
        if (isFound == null)
        {
            return false;
        }
        
        token = new SyntaxToken
        {
            Name = "KEYWORD:" + isFound.Name,
            Symbol = isFound.Value,
            TokenType = TokenType.KeywordToken,
            Position = current.Position
        };
        Advance();
        return true;
    }

    
    private bool HandleLogic(out SyntaxToken token)
    {
        token = null;
        var current = Current();
        if ((current.Symbol == "&" && Peek(1).Symbol == "&") || current.Symbol == "and")
        {
            if (current.Symbol == "and")
            {
                Advance();
            }
            else
            {
                Advance();
                Advance();
            }
            token = new SyntaxToken
            {
                Name = "Logic:and",
                Symbol = "and",
                TokenType = TokenType.BinaryToken,
                Position = current.Position
            };
            return true;
        }
    
        if ((current.Symbol == "|" && Peek(1).Symbol == "|") || current.Symbol == "or")
        {

            if (current.Symbol == "or")
            {
                Advance();
            }
            else
            {
                Advance();
                Advance();
            }
            
            token = new SyntaxToken
            {
                Name = "Logic:or",
                Symbol = "or",
                TokenType = TokenType.BinaryToken,
                Position = current.Position
            };
            return true;
        }
        
      
        return false;
    }
    
}