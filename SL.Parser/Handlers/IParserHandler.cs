using SL.Parser.Api;
using SL.Parser.Parsing.AST;

namespace SL.Parser.Parsing;

public interface IParserHandler<T>
{
    public Task<T> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters);
}