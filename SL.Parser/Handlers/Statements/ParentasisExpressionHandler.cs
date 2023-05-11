using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Expressions;

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