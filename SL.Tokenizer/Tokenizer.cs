using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Iterators;
using SL.Tokenizer.Models;

namespace SL.Tokenizer;

public class Tokenizer
{
    private readonly CharIterator _iterator;
    private readonly Dictionary<string, ITokenHandler> _handlers;
    private readonly HashSet<string> _ignores;

    public Tokenizer(CharIterator iterator,
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
                return new Token(TokenType.IDENTIFIER, symbol, _iterator.Position());
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
            if (token.Type == TokenType.IGNORED)
            {
                continue;
            }

            tokens.Add(token);
        } while (!ctx.IsCancellationRequested &&
                 (token.Type != TokenType.END_OF_FILE && token.Type != TokenType.BAD_TOKEN));

        var cleared = new List<Token>();
        for (var i = 1; i < tokens.Count(); i++)
        {
            var one = tokens[i - 1];
            var two = tokens[i];


            if (one.Value == string.Empty || two.Value == string.Empty)
            {
                cleared.Add(one);
                continue;
            }
            
            
            var value = one.Value + two.Value;
            if (_handlers.ContainsKey(value))
            {
                var handler = _handlers[value];
                var tokenaa = await handler.Handle(value, _iterator, ctx);
                cleared.Add(tokenaa);
                i += 1;
                continue;
            }
            
            cleared.Add(one);
        }


        return cleared;
    }

    public async Task<TokenIterator> LexAllToInterator(CancellationToken ctx = new CancellationToken())
    {
        return new TokenIterator(await LexAll(ctx), Token.EndOfFile(null), ctx);
    }
}