using SL.Parser.Models.Literals;
using SL.Parser.Models.Statements;
using SL.Parser.Models.Statements.Declarations;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements;

public class VariableHandler : IParserHandler<VariableStatement>
{
    
    public async Task<VariableStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        var variableType = await parserFactory.CreateNode<IdentifierLiteral>();
        var declaration = await GetVariableDecrlarationStatementList(tokenIterator, parserFactory);
        return new VariableStatement(variableType, declaration);
    }
    
    
    private async Task<List<VariableDeclarationStatement>> GetVariableDecrlarationStatementList(ITokenIterator tokenIterator, NodeFactory parserFactory)
    {
        var declarations = new List<VariableDeclarationStatement>();
        do
        {
            if (tokenIterator.LookUp().Type == TokenType.COMMA)
            {
                tokenIterator.NextToken(TokenType.COMMA);
            }

            var declaration = await parserFactory.CreateNode<VariableDeclarationStatement>();
            declarations.Add(declaration);
        } while (tokenIterator.LookUp().Type == TokenType.COMMA);


        return declarations;
    }
}