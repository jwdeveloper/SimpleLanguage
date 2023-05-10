using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Statements.Declarations;

public class FunctionDeclarationHandler : IParserHandler<FunctionDeclarationStatement>
{
    public async Task<FunctionDeclarationStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] param)
    {
        tokenIterator.NextToken("function");
        
        //todo
        var functionType = tokenIterator.LookUp().Type!= TokenType.OBJECT_TYPE? null: await parserFactory.CreateNode<IdentifierLiteral>("ignore");

        
        var functionName = await parserFactory.CreateNode<IdentifierLiteral>("ignore");
        var parameters = await GetParameters(tokenIterator, parserFactory);
        var body = await parserFactory.CreateNode<BlockStatement>();

        return new FunctionDeclarationStatement(functionName, functionType, parameters, body);
    }
    
    
    private async Task<List<ParameterStatement>> GetParameters(ITokenIterator tokenIterator, NodeFactory parserFactory)
    {
        var listParameter = new List<ParameterStatement>();
        tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);

        while (tokenIterator.LookUp().Type != TokenType.CLOSE_ARGUMENTS)
        {
            var parameterType = tokenIterator.LookUp().Type!= TokenType.OBJECT_TYPE? null: await parserFactory.CreateNode<IdentifierLiteral>();;
            var parameterName = await parserFactory.CreateNode<IdentifierLiteral>();

            var parameter = new ParameterStatement(parameterType, parameterName);
            listParameter.Add(parameter);

            if (tokenIterator.LookUp().Type == TokenType.COMMA)
            {
                tokenIterator.NextToken(TokenType.COMMA);
                continue;
            }

            break;
        }

        tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);
        return listParameter;
    }
}