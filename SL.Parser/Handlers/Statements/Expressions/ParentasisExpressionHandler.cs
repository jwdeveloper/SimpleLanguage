using SL.Parser.Handlers.Expressions;
using SL.Parser.Models;
using SL.Parser.Models.Statements;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements.Expressions;

public class ParentasisExpressionHandler : IParserHandler<ParentasisExpressionStatement>
{
    public async Task<ParentasisExpressionStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {
        tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);
        var expression = await parserFactory.CreateNode<Expression>(ExpressionHandler.ExpressionLookup.Binary);
        tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);
        return new ParentasisExpressionStatement(expression);
    }
}