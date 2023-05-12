using SL.Parser.Models;
using SL.Parser.Models.Statements;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements.Expressions;

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