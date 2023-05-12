using SL.Parser.Handlers.Expressions;
using SL.Parser.Models;
using SL.Parser.Models.Literals;
using SL.Parser.Models.Statements.Declarations;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements.Declarations;

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