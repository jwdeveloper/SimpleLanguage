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
        var parameters = await parserFactory.CreateNode<List<ParameterStatement>>(tokenIterator, parserFactory);
        var body = await parserFactory.CreateNode<BlockStatement>();

        return new FunctionDeclarationStatement(functionName, functionType, parameters, body);
    }
    
    
 
}