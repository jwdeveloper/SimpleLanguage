using SL.Parser.Models;
using SL.Parser.Models.Statements;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements;

public class ReturnHandler : IParserHandler<ReturnStatement>
{
    public async Task<ReturnStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        tokenIterator.NextToken("return");
        var expression = tokenIterator.LookUp().Type == TokenType.END_OF_LINE ? null : await parserFactory.CreateNode<Expression>();
        tokenIterator.NextToken(TokenType.END_OF_LINE);
        return new ReturnStatement(expression);
    }
}