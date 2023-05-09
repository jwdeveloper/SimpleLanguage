using SL.Parser.Api;
using SL.Parser.Common;

namespace SL.Parser.Lexing;

public class LexerBuilder
{
    private Dictionary<string, ITokenHandler> _handlers;
    private HashSet<string> _ignores;
    private string _content;
    private string _fileName;

    public LexerBuilder()
    {
        _ignores = new HashSet<string>();
        _handlers = new Dictionary<string, ITokenHandler>();
        _content = string.Empty;
        _fileName = string.Empty;
    }

    public LexerBuilder WithFileName(string fileName)
    {
        _fileName = fileName;
        return this;
    }
    
    public LexerBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }
    
    public LexerBuilder WithSymbol(string symbol, TokenType tokenType)
    {
        _handlers.Add(symbol, new BasicTokenHandler(tokenType));
        return this;
    }
    
    public LexerBuilder WithSymbol(string[] symbols, TokenType tokenType)
    {
        foreach(var symbol in symbols)
        {
            WithSymbol(symbol, tokenType); 
        }
        return this;
    }
    
    public LexerBuilder WithIgnore(string symbol)
    {
        _ignores.Add(symbol);
        return this;
    }

    public LexerBuilder WithSymbol(string symbol, ITokenHandler tokenHandler)
    {
        _handlers.Add(symbol, tokenHandler);
        return this;
    }
    
    public LexerBuilder WithSymbol(string[] symbols, ITokenHandler tokenHandler)
    {
        foreach(var symbol in symbols)
        {
            WithSymbol(symbol, tokenHandler); 
        }
        return this;
    }
    
    public Lexer Build()
    {
        var position = new Position();
        var target = _content.ToCharArray();
        var iterator = new CharIterator(target, position);
        return new Lexer(iterator, _handlers,_ignores);
    }
}