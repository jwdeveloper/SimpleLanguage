using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST.Statements;

namespace SL.Parser.Parsing.Handlers.Statements;

public class BreakHandler : IParserHandler<BreakStatement>
{
    public async Task<BreakStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        tokenIterator.NextToken("break");
        tokenIterator.NextToken(TokenType.END_OF_LINE);
        return new BreakStatement();
    }
}