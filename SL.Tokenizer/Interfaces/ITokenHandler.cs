using SL.Parser.Common;

namespace SL.Parser.Api;

public interface ITokenHandler
{
    public Task<Token> Handle(string symbol, IIterator<char> iterator, CancellationToken ctx);
}