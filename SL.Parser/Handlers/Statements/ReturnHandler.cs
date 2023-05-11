using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Statements;

namespace SL.Parser.Parsing.Handlers.Statements;

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