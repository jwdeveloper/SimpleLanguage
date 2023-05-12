using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Tokenizer.Handlers;

public class BasicTokenHandler : ITokenHandler
{
    private readonly TokenType _tokenType;
    public BasicTokenHandler(TokenType tokenType)
    {
        _tokenType = tokenType;
    }
 
    public Task<Token> Handle(string symbol, IIterator<char> iterator, CancellationToken ctx)
    {
        return Task.FromResult(new Token(_tokenType, symbol.ToString(), iterator.Position()));
    }
}