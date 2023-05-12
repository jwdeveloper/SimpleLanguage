using SL.Tokenizer.Models;

namespace SL.Test.Unit.Tokenizer;

public class TokenizerAssert
{
    public static TokenizerAssert AssertTokens(List<Token> tokens)
    {
        return new TokenizerAssert(tokens);
    }

    private List<Token> _tokens;

    public TokenizerAssert(List<Token> tokens)
    {
       _tokens = tokens;
    }

    public TokenizerAssert HasToken(TokenType tokenType, int count =1)
    {
        var token = _tokens.Where(e => e.Type == tokenType);
        Assert.That(token.Count(), Is.EqualTo(count));
        return this;
    }

    public TokenizerAssert HasToken(TokenType tokenType, string value, int index = -1)
    {
        var token = _tokens.First(e => e.Type == tokenType && e.Value == value);
        Assert.NotNull(token);
        if (index != -1)
        {
            Assert.That(_tokens[index], Is.EqualTo(token));
        }
        
        return this;
    }
}