namespace SimpleLangInterpreter.Core;

public class TokenIterator
{
    private readonly List<SyntaxToken> tokens;
    private SyntaxToken currnet;
    private int position;

    public TokenIterator(List<SyntaxToken> tokens)
    {
        this.tokens = tokens;
        this.position = -1;
        this.currnet = null;
    }

    public SyntaxToken Current()
    {
        return currnet;
    }
    
    public SyntaxToken Advance()
    {
        position++;
        if (position >= tokens.Count)
        {
            currnet = SyntaxToken.END_OF_FILE();
            return currnet;
        }
        currnet = tokens[position];
        return currnet;
    }

    public bool IsValid()
    {
        return currnet != null && currnet.TokenType != TokenType.EndOfFile;
    }
    
    public SyntaxToken Peek(int offset)
    {
        var index = position + offset;

        if (index >= tokens.Count)
            return SyntaxToken.END_OF_FILE();

        return tokens[index];
    }

    public void Reset()
    {
        currnet = null;
        position = -1;
    }
}