using SL.Parser.Api;
using SL.Parser.Api.Exceptions;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;

namespace SL.Parser.Parsing.Handlers.Statements;

public class StatementListHandler : IParserHandler<List<Statement>>
{
    public async Task<List<Statement>> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {
        if (parameters.Length != 1)
        {
            throw new Exception("StatementListHandler require TokenType as 1 parameters, but was 0");
        }
        var stopLookAhead = (TokenType)parameters[0];

        var statements = new List<Statement>();
        try
        {
            while (tokenIterator.IsValid() && tokenIterator.LookUp().Type != stopLookAhead)
            {
                var statement = await parserFactory.CreateNode<Statement>();
                statements.Add(statement);
            }
        }
        catch (EndOfParsingException e)
        {
            return statements;
        }

        return statements;
    }
}