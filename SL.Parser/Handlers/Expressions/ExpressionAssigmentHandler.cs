using SL.Parser.Api;
using SL.Parser.Api.Exceptions;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Expressions;

public class ExpressionAssigmentHandler : IParserHandler<Expression>
{
    public async Task<Expression> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {
        var left = await parserFactory.CreateNode<Expression>(ExpressionHandler.ExpressionLookup.Binary);
        if (!IsAssigmentType(tokenIterator.LookUp()))
        {
            return left;
        }

        if (!(left is IdentifierLiteral))
        {
            throw new SyntaxException($"to assigned type must be of type {TokenType.IDENTIFIER}",
                tokenIterator.CurrentToken());
        }

        var assigmentToken = tokenIterator.NextToken(TokenType.ASSIGMENT, TokenType.COMPLEX_ASSIGMENT);

        var right = await parserFactory.CreateNode<Expression>();
        return new AssigmentExpression(assigmentToken, left, right);
    }
    
    private bool IsAssigmentType(Token token)
    {
        return token.Type is TokenType.ASSIGMENT or TokenType.COMPLEX_ASSIGMENT;
    }

}