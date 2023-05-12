using SL.Parser.Models.Expressions;
using SL.Parser.Models.Literals;
using SL.Tokenizer.Exceptions;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Expressions.Literals;

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