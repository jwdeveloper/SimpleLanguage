using SL.Tokenizer.Exceptions;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Tokenizer.Iterators;

public class TokenIterator : AbstractIterator<Token>, ITokenIterator
{

    private readonly CancellationToken _ctx;
    
    
    public TokenIterator(List<Token> target, Token defaultValue, CancellationToken cancellationToken) : base(target, defaultValue)
    {
        _ctx = cancellationToken;
    }
    
    public Token NextToken()
    {
        return ValiateToken(Advance());
    }
    
    public Token NextToken(string value)
    {
        var token = Advance();
        if (token.Value != value)
        {
            throw new BadTokenException(value, token);
        }
        return ValiateToken(token);
    }

    public Token NextToken(string value, TokenType type)
    {
        var token = Advance();
        if (token.Value != value)
        {
            throw new BadTokenException(value, token);
        }
        if (token.Type != type)
        {
            throw new BadTokenException(value, token);
        }
        return ValiateToken(token);
    }


    public Token NextToken(params TokenType[] types)
    {
        var token = Advance();

        var hasType = types.Any(e => e == token.Type);
        if (!hasType)
        {
            throw new BadTokenException(types[0].ToString(), token);   
        }
        return ValiateToken(token);
    }

    public Token CurrentToken()
    {
        return ValiateToken(Current());
    }

    public Token LookUp()
    {
        return Peek(1);
    }

    public bool LookUp(string value)
    {
        return Peek(1).Value == value;
    }

    public bool LookUp(TokenType value)
    {
        return Peek(1).Type == value;
    }

    public new bool IsValid()
    {
        if (_ctx.IsCancellationRequested)
        {
            return false;
        }

        return true;
    }

    public List<Token> ToList()
    {
       return _target;
    }

 

    private Token ValiateToken(Token token)
    {
        if (_ctx.IsCancellationRequested)
            throw new EndOfParsingException("Cancellation request");

        if (token.Type == TokenType.END_OF_FILE)
            throw new EndOfParsingException("End of file");

        if (token.Type == TokenType.BAD_TOKEN)
            throw new EndOfParsingException("Bad token");

        return token;
    }
}