using SL.Parser.Common;

namespace SL.Parser.Api;

public interface ITokenIterator
{
    public Token NextToken();

    public Token NextToken(string value);
    
    public Token NextToken(params TokenType[] type);

    public Token CurrentToken();

    public Token LookUp();
    
    public Token LookUp(string value);

    public Token LookUp(TokenType value);

    public bool IsValid();
    public List<Token> ToList();
}