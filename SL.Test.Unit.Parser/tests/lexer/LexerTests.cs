namespace SL.Test.Unit.Parser.tests.lexer;

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