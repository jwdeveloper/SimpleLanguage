using SL.Parser.Models.Literals;
using SL.Parser.Models.Statements;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements.Declarations;

public class ParameterStatementListHandler : IParserHandler<List<ParameterStatement>>
{
    public async Task<List<ParameterStatement>> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
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