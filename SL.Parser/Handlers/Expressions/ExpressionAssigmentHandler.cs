using SL.Parser.Models;
using SL.Parser.Models.Expressions;
using SL.Parser.Models.Literals;
using SL.Tokenizer.Exceptions;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Expressions;

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