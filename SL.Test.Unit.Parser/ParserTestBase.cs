using SL.Parser;
using SL.Parser.Models;
using SL.Tokenizer;

namespace SL.Test.Unit.Parser;

public class ParserTestBase
{
    
    protected async Task<SlProgram> CreateProgramTree(string content)
    {
        var lexer = CreateLexer(content);
        var iterator = await lexer.LexAllToInterator();
        return await ParserFactory.CreateParser(iterator, new CancellationToken()).ParseNew();
    }

   

    protected  Tokenizer.Tokenizer CreateLexer(string content)
    {
        return TokenizerFactory.CreateTokenizer(content);
    }
}