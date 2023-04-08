 







using SL.Core.Api;
using SL.Core.Common;

namespace SL.Core.Lexing;

public class Lexer
{
    private CharIterator _iterator;
    private Dictionary<string, ITokenHandler> _handlers;
    private HashSet<string> _ignores;

    public Lexer(CharIterator iterator,
                 Dictionary<string, ITokenHandler> handlers,
                 HashSet<string> ignores)
    {
        _iterator = iterator;
        _handlers = handlers;
        _ignores = ignores;
    }

    public async Task<Token> Lex(CancellationToken ctx)
    {
        _iterator.Advance();
        if (_iterator.IsEnded())
        {
            return Token.EndOfFile(_iterator.Position());
        }

        var symbol = string.Empty;
        while (!_iterator.IsEnded())
        {
            if (ctx.IsCancellationRequested)
            {
                return Token.Canceled(_iterator.Position());
            }

            symbol += _iterator.Current().ToString();
            if (_ignores.Contains(symbol))
            {
                return new Token(TokenType.IGNORED, symbol, _iterator.Position());
            }

            if (_handlers.ContainsKey(symbol))
            {
                var handler = _handlers[symbol];
                return await handler.Handle(symbol, _iterator, ctx);
            }

            var nextSymbol = _iterator.Peek(1).ToString();
            if (_handlers.ContainsKey(nextSymbol) || _ignores.Contains(nextSymbol))
            {
                return new Token(TokenType.LITERRAL, symbol, _iterator.Position());
            }

            _iterator.Advance();
        }

        if (_iterator.IsEnded())
        {
            return Token.EndOfFile(_iterator.Position());
        }

        return Token.BadToken($"Unknown symbol: {symbol}", _iterator.Position());
    }

    public async Task<List<Token>> LexAll(CancellationToken ctx = new CancellationToken())
    {
        var tokens = new List<Token>();
        Token token;
        do
        {
            token = await Lex(ctx);
            tokens.Add(token);
        } 
        while (token.Type != TokenType.END_OF_FILE && token.Type != TokenType.BAD_TOKEN);

        return tokens;
    }
}