using SL.Parser.Api;
using SL.Parser.Lexing;
using SL.Parser.Parsing.AST;


namespace SL.Parser.Parsing;

public class ParserBuilder
{
    private readonly Dictionary<Type, object> _parserHandlers;
    private readonly CancellationToken _ctx;
    private readonly ITokenIterator _tokenIterator;
    public ParserBuilder(ITokenIterator tokenIterator,  CancellationToken ctx)
    {
        _parserHandlers = new Dictionary<Type, object>();
        _tokenIterator = tokenIterator;
        _ctx = ctx;

    }
    
    public ParserBuilder WithParserHandler<ReturnNode, Handler>()   where Handler : new() 
    {
        var handler = new Handler();
        var nodeReturnType = typeof(ReturnNode);
        _parserHandlers.Add(nodeReturnType, handler);
        return this;
    }
    
    public Parser Build()
    {
        var nodeFactory = new NodeFactory(_parserHandlers, _tokenIterator, _ctx);
        return new Parser(nodeFactory);
    }
}