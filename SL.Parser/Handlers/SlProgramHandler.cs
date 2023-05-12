using SL.Parser.Models;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers;

public class SlProgramHandler : IParserHandler<SlProgram>
{
    public async Task<SlProgram> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
         var statements = await parserFactory.CreateNode<List<Statement>>(TokenType.END_OF_FILE);
         return new SlProgram(statements);
    }
}