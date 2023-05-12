using SL.Tokenizer.Models;

namespace SL.Tokenizer.Interfaces;

public interface ITokenIterator
{
    public Token NextToken();

    public Token NextToken(string value);
    
    public Token NextToken(string value, TokenType type);
    
    public Token NextToken(params TokenType[] type);

    public Token CurrentToken();

    public Token LookUp();

    public bool LookUp(string value);
    public bool LookUp(TokenType value);

    public bool IsValid();
    public List<Token> ToList();
}