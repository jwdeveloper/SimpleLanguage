using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;
using SL.Parser.Parsing.Handlers.Expressions;

namespace SL.Parser.Parsing.Handlers.Statements.Declarations;

public class VariableDeclarationHandler : IParserHandler<VariableDeclarationStatement>
{
    public async Task<VariableDeclarationStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {
        var identifier = await parserFactory.CreateNode<IdentifierLiteral>();
        if (tokenIterator.LookUp().Type == TokenType.END_OF_FILE)
        {
            return new VariableDeclarationStatement(identifier, null);
        }

        if (tokenIterator.LookUp().Type == TokenType.ASSIGMENT)
        {
            tokenIterator.NextToken(TokenType.ASSIGMENT);
            var assigment = await parserFactory.CreateNode<Expression>(ExpressionHandler.ExpressionLookup.Assigment);
            return new VariableDeclarationStatement(identifier, assigment);
        }


        return new VariableDeclarationStatement(identifier, null);
    }
    
 
}