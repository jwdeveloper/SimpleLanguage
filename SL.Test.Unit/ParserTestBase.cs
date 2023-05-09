using SL.Core;
using SL.Core.Common;
using SL.Core.Lexing;
using SL.Core.Lexing.Handlers;
using SL.Core.Parsing;
using SL.Core.Parsing.AST;

namespace SL.Test.Unit;

public class ParserTestBase
{
    public Parser CreateParser()
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