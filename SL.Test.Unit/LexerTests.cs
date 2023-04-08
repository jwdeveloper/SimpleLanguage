using System.Text.RegularExpressions;
using SL.Core.Common;
using SL.Core.Lexing;
using SL.Core.Lexing.Handlers;

namespace SL.Test.Unit;

public class LexerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var content = @"
        +-*/^=

        {}[]();

        1
        .10
        1.10
        
        ""This is text""

        "" This is Bad "" Token ""
        ";
        
        
        var builder = new LexerBuilder();
        builder.WithContent(content);
        builder.WithSymbol("+", TokenType.BINARY_OPERATION);
        builder.WithSymbol("-", TokenType.BINARY_OPERATION);

        var lexer = builder.Build();
    }
    
    
    
    [Test]
    public async Task ShouldParseTokens()
    {
        var i = 1.3213;
        var content = " Hello +. - World \"siema\" is in for while\n";
        
        var builder = new LexerBuilder();
        builder.WithContent(content);
        builder.WithIgnore(" ");
        builder.WithIgnore("\n");
        
        builder.WithSymbol("+", TokenType.BINARY_OPERATION);
        builder.WithSymbol("-", TokenType.BINARY_OPERATION);
        builder.WithSymbol("=", TokenType.EQUAL);
        builder.WithSymbol(";", TokenType.END_OF_LINE);
        builder.WithSymbol("\"", new StringHandler());
        builder.WithSymbol(new[]{"for","while","in","of"}, TokenType.KEYWORLD);
        builder.WithSymbol(new []{"1","2","3","4","5","6","7","8","9","0","."}, new NumberHandler());
        var lexer = builder.Build();
        var tokens = await lexer.LexAll();

        var x = 12;
    }
}