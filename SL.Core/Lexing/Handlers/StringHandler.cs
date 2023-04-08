using System.Text;
using SL.Core.Api;
using SL.Core.Common;

namespace SL.Core.Lexing.Handlers;

public class StringHandler : ITokenHandler
{
    private readonly char _closeTag;

    public StringHandler(char closeTag = '"')
    {
        _closeTag = closeTag;
    }
    
    
    public async Task<Token> Handle(string symbol, IIterator<char> iterator, CancellationToken ctx)
    {
        var sb = new StringBuilder();
        while (!iterator.IsEnded())
        {
            if (ctx.IsCancellationRequested)
            {
                return Token.Canceled(iterator.Position());
            }
            
            var current = iterator.Advance();
            if (current != _closeTag)
            {
                sb.Append(current);
                continue;
            }
            return new Token(TokenType.STRING, sb.ToString(), iterator.Position());
           
        }
        
        return Token.BadToken($"String is not closed: {sb}", iterator.Position());
    }
}