using SL.Parser.Models.Literals;
using SL.Parser.Models.Statements;
using SL.Parser.Models.Statements.Blocks;
using SL.Parser.Models.Statements.Declarations;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements.Declarations;

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