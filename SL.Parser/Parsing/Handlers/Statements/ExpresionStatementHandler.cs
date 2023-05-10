using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Statements;

public class ExpresionStatementHandler : IParserHandler<ExpresionStatement>
{
    public async Task<ExpresionStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {
        var expression = await parserFactory.CreateNode<Expression>();
        tokenIterator.NextToken(TokenType.END_OF_LINE);
        return new ExpresionStatement(expression);
    }
}