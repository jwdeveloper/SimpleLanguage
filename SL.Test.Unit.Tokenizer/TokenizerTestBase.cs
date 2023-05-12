using SL.Tokenizer;
using SL.Tokenizer.Models;

namespace SL.Test.Unit.Tokenizer;

public class TokenizerTestBase
{
    protected SL.Tokenizer.Tokenizer CreateTokenizer(string content)
    {
        return TokenizerFactory.CreateTokenizer(content);
    }
    
    protected async Task<List<Token>> CreateTokens(string content)
    {
        var tokenizer = CreateTokenizer(content);

        return await tokenizer.LexAll();
    }
    
}