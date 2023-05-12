using SL.Parser.Models.Statements;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements;

public class BreakHandler : IParserHandler<BreakStatement>
{
    public async Task<BreakStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        tokenIterator.NextToken("break");
        tokenIterator.NextToken(TokenType.END_OF_LINE);
        return new BreakStatement();
    }
}