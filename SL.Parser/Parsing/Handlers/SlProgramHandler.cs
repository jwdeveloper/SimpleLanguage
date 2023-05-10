using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;

namespace SL.Parser.Parsing.Handlers;

public class SlProgramHandler : IParserHandler<SlProgram>
{
    public async Task<SlProgram> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
         var statements = await parserFactory.CreateNode<List<Statement>>(TokenType.END_OF_FILE);
         return new SlProgram(statements);
    }
}