using NUnit.Framework;
using SimpleLangInterpreter.Core;

namespace Test.Unit;

public class Tests
{
    public string input;
    
    
    [SetUp]
    public void Setup()
    {
        input = @"var i
        = 0
        ;
        ";
    }

    [Test]
    public void ShouldGiveRightAmountOfTokens()
    {
        var lexer = new Lexer("test.jw", input);
        var tokens = lexer.CreateTokens();
        Assert.AreEqual(5, tokens.Count);
    }
}