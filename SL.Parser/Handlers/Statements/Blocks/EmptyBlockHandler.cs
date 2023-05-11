using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;

namespace SL.Parser.Parsing.Handlers.Statements.Blocks;

public class EmptyBlockHandler : IParserHandler<EmptyBlockStatement>
{
    public async Task<EmptyBlockStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {
        tokenIterator.NextToken(TokenType.END_OF_LINE);
        return new EmptyBlockStatement();
    }
}