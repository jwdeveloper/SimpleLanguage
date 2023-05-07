using SL.Core.Api;
using SL.Core.Api.Exceptions;
using SL.Core.Common;

namespace SL.Core.Lexing;

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

    public Token LookUp(string value)
    {
        var token = Peek(1);
        if (token.Value != value)
        {
            throw new BadTokenException(value, token);
        }

        return token;
    }

    public Token LookUp(TokenType value)
    {
        var token = Peek(1);
        if (token.Type != value)
        {
            throw new BadTokenException(value.ToString(), token);
        }

        return token;
    }

    public new bool IsValid()
    {
        if (_ctx.IsCancellationRequested)
        {
            return false;
        }

        return true;
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