using SL.Parser;
using SL.Parser.Lexing;
using SL.Parser.Parsing.AST;

namespace SL.Test.Unit;

public class ParserTestBase
{
    public Parser.Parsing.Parser CreateParser()
    {
        return ParserFactory.CreateParser();
    }


    
    protected async Task<SlProgram> CreateProgram(string content)
    {
        var lexer = CreateLexer(content);
        var iterator = await lexer.LexAllToInterator();
        return ParserFactory.CreateParser().Parse(iterator);
    }

    protected  Lexer CreateLexer(string content)
    {
        return LexerFactory.CreateLexer(content);
    }
}