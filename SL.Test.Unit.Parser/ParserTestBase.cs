using SL.Parser;
using SL.Parser.Lexing;
using SL.Parser.Parsing.AST;

namespace SL.Test.Unit.Parser;

public class ParserTestBase
{
    
    protected async Task<SlProgram> CreateProgramTree(string content)
    {
        var lexer = CreateLexer(content);
        var iterator = await lexer.LexAllToInterator();
        return await ParserFactory.CreateParser(iterator, new CancellationToken()).ParseNew();
    }

   

    protected  Tokenizer CreateLexer(string content)
    {
        return TokenizerFactory.CreateLexer(content);
    }
}