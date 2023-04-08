using SL.Core.Common;

namespace SL.Core.Api;

public interface ITokenHandler
{
    public Task<Token> Handle(string symbol, IIterator<char> iterator, CancellationToken ctx);
}