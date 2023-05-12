using SL.Tokenizer.Interfaces;

namespace SL.Parser.Handlers;

public interface IParserHandler<T>
{
    public Task<T> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters);
}