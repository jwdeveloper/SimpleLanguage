using SL.Tokenizer.Models;

namespace SL.Tokenizer.Interfaces;

public interface ITokenHandler
{
    public Task<Token> Handle(string symbol, IIterator<char> iterator, CancellationToken ctx);
}