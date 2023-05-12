using SL.Parser.Models.Statements;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements.Blocks;

public class EmptyBlockHandler : IParserHandler<EmptyBlockStatement>
{
    public async Task<EmptyBlockStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {
        tokenIterator.NextToken(TokenType.END_OF_LINE);
        return new EmptyBlockStatement();
    }
}