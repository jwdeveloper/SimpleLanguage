using SL.Tokenizer.Models;

namespace SL.Test.Unit.Tokenizer.Tests;

public class StringsTests : TokenizerTestBase
{


    [Test]
    public async Task ShouldParseString()
    {
        var tokens = await CreateTokens(" \"hello\" ");
        TokenizerAssert.AssertTokens(tokens).HasToken(TokenType.STRING);
    }
    
    
    [Test]
    public async Task ShouldParseEmptyString()
    {
        var tokens = await CreateTokens(" ;\"\"; ");
        TokenizerAssert.AssertTokens(tokens).HasToken(TokenType.STRING);
    }
    
}