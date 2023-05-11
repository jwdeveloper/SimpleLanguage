using SL.Parser.Api;
using SL.Parser.Api.Exceptions;
using SL.Parser.Common;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Expressions.Literals;

public class IdetifierHandler : IParserHandler<IdentifierLiteral>
{
    public async Task<IdentifierLiteral> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {
        var nextToken = tokenIterator.LookUp();
        if (nextToken.Type is not (TokenType.IDENTIFIER or TokenType.OBJECT_TYPE or TokenType.KEYWORLD))
        {
            throw new SyntaxException("Expected identifier", tokenIterator.LookUp());
        }

        var token = tokenIterator.NextToken();

        if (parameters.Length == 1 && parameters[0].Equals("ignore"))
        {
            return new IdentifierLiteral(token.Value);
        }
        
        switch (tokenIterator.LookUp().Type)
        {
            case TokenType.DOT:
            {
                tokenIterator.NextToken(TokenType.DOT);
                var nextLiteral = await Parse(tokenIterator, parserFactory, parameters);
                return new IdentifierLiteral(token.Value, nextLiteral);
            }
            case TokenType.OPEN_ARGUMETNS:
                return await parserFactory.CreateNode<FunctionCallExpression>(new IdentifierLiteral(token.Value));
            
            default:
                return new IdentifierLiteral(token.Value);
        }
    }


  
}