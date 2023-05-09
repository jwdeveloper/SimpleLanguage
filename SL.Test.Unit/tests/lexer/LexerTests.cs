using System.Text.RegularExpressions;
using SL.Parser.Common;
using SL.Parser.Lexing;
using SL.Parser.Lexing.Handlers;

namespace SL.Test.Unit;

public class LexerTests : ParserTestBase
{
    [Test]
    public async Task ShouldParseTokens()
    {
        var content = " Hello +. - World \"siema\" is in for while number\n";

        var lexer = CreateLexer(content);

        
    }
    
    
    [Test]
    public async Task ShouldParseTokens2()
    {
        var content = "+==mike234";

        var lexer = CreateLexer(content);


        var result = await lexer.LexAll();
        var i = 0;
    }
}