using SL.Parser.Api;
using SL.Parser.Api.Exceptions;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Expressions;

public class FunctionCallHandler : IParserHandler<FunctionCallExpression>
{
    public async Task<FunctionCallExpression> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        if (parameters.Length != 1)
        {
            throw new SyntaxException("Function call need to have Name identifier", tokenIterator.CurrentToken());
        }

        var functionName = (IdentifierLiteral)parameters[0];
        var functionCallParameters = await GetFunctionCallParameters(tokenIterator, parserFactory);

        if (tokenIterator.LookUp().Type == TokenType.DOT)
        {
            tokenIterator.NextToken(TokenType.DOT);
            var nextCall = await parserFactory.CreateNode<IdentifierLiteral>();
            return new FunctionCallExpression(functionName, functionCallParameters, nextCall);
        }


        return new FunctionCallExpression(functionName, functionCallParameters);
    }
    
    
    private async Task<List<Expression>> GetFunctionCallParameters(ITokenIterator tokenIterator, NodeFactory parserFactory)
    {
        var expressions = new List<Expression>();
        tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);
        if (tokenIterator.LookUp().Type == TokenType.CLOSE_ARGUMENTS)
        {
            tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);
            return expressions;
        }
        do
        {
            var expression = await parserFactory.CreateNode<Expression>();
            expressions.Add(expression);
        } while (tokenIterator.LookUp().Type == TokenType.COMMA && tokenIterator.NextToken(TokenType.COMMA) != null);

        tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);
        return expressions;
    }
    
   
}