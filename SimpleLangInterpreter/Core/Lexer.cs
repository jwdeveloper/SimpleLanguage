using System.Text;
using SimpleLangInterpreter.Exceptions;

namespace SimpleLangInterpreter.Core;

public class Lexer
{
    private readonly string _fileName;
    private readonly char[] _input;
    private Position position;
    private char? _currentChar;
    private readonly List<DefaultToken> _defaultTokens;
    private readonly List<DefaultOperations> _defaultOperations;
    private readonly List<DefaultType> _defaultTypes;
    private readonly List<DefaultKeywords> _defaultKeywords;

    public Lexer(string fileName, string input)
    {
        _fileName = fileName;
        _input = input.ToCharArray();
        position = new Position(-1, 0, -1, fileName, input);
        _defaultTokens = DefaultToken.GetDefaultTokens();
        _defaultOperations = DefaultOperations.GetDefaultOperations();
        _defaultTypes = DefaultType.GetDefaultTypes();
        _defaultKeywords = DefaultKeywords.GetDefaultTypes();
    }

    private void Advance()
    {
        position.advance(_currentChar);
        _currentChar = position.Index < _input.Length ? _input[position.Index] : null;
    }

    private char Lookahead => Peek(1);

    private char Peek(int offset)
    {
        var index = position.Index + offset;

        if (index >= _input.Length)
            return '\0';

        return _input[index];
    }


    public List<SyntaxToken> CreateTokens()
    {
        var tokens = new List<SyntaxToken>();
        Advance();
        var litteral = string.Empty;
        while (_currentChar != null)
        {
            var token = LookDefaultTokens();
            if (token == null)
            {
                litteral += _currentChar;
            }
            else
            {
                if (litteral != string.Empty)
                {
                    var literralTokens = ReadLitteral(litteral);
                    tokens.AddRange(literralTokens);
                    litteral = string.Empty;
                }

                tokens.Add(token);
            }

            Advance();

            if (_currentChar == null && litteral != string.Empty)
            {
                tokens.AddRange(ReadLitteral(litteral));
            }
        }

        return tokens;
    }

    public SyntaxToken? LookDefaultTokens()
    {
        foreach (var defaultToken in _defaultTokens)
        {
            if (defaultToken.Symbol != _currentChar)
            {
                continue;
            }

            if (defaultToken == DefaultToken.QUOTE)
            {
                return ReadString();
            }

            if (_currentChar == DefaultToken.DIVIDE.Symbol && Lookahead == DefaultToken.DIVIDE.Symbol)
            {
                return ReadComment();
            }

            if (_currentChar == DefaultToken.DIVIDE.Symbol && Lookahead == '*')
            {
                return ReadLongComment();
            }

            return new SyntaxToken()
            {
                Name = defaultToken.Name,
                Symbol = defaultToken.Symbol.ToString(),
                TokenType = defaultToken.TokenType,
                Position = position.copy(),
            };
        }
        return null;
    }


    public List<SyntaxToken> ReadLitteral(string currentText)
    {
        var result = new List<SyntaxToken>();
        currentText = currentText.Replace('\r', ' ').Replace('\n', ' ');
        var values = currentText.Split(' ');
        foreach (var value in values)
        {
            if (value == string.Empty || value == "\r\n")
            {
                continue;
            }

            if (isNumber(value))
            {
                result.Add(new SyntaxToken
                {
                    Name = "NUMBER:INT",
                    Symbol = value,
                    TokenType = TokenType.NumberToken
                });
                continue;
            }

            result.Add(new SyntaxToken
            {
                Name = "LITTERAL",
                Symbol = value,
                TokenType = TokenType.LitteralToken
            });
        }
        return result;
    }

    public bool isNumber(string input)
    {
        var dots = 0;
        foreach (var charr in input.ToCharArray())
        {
            if (!char.IsNumber(charr) && charr != DefaultToken.DOT.Symbol)
            {
                return false;
            }

            if (charr == DefaultToken.DOT.Symbol)
            {
                dots++;
            }

            if (dots > 1)
            {
                return false;
            }
        }

        return true;
    }

    private SyntaxToken ReadComment()
    {
        Advance();
        Advance();
        var comment = new StringBuilder();
        while (_currentChar != null && _currentChar != '\n' && _currentChar != '\r')
        {
            comment.Append(_currentChar);
            Advance();
        }
        return new SyntaxToken
        {
            Name = "Comment",
            Symbol = comment.ToString(),
            Position = position.copy(),
            TokenType = TokenType.Comment
        };
    }

    private SyntaxToken ReadLongComment()
    {
        Advance();
        Advance();
        var comment = new StringBuilder();
        while (_currentChar != null)
        {
            if (_currentChar == '*' && Lookahead == DefaultToken.DIVIDE.Symbol)
            {
                Advance();
                break;
            }

            comment.Append(_currentChar);
            Advance();
        }

        return new SyntaxToken
        {
            Name = "Comment",
            Symbol = comment.ToString(),
            Position = position.copy(),
            TokenType = TokenType.Comment
        };
    }


    private SyntaxToken ReadString()
    {
        Advance();
        var sb = new StringBuilder();
        var done = false;

        while (!done)
        {
            switch (_currentChar)
            {
               
                case '"':
                    if (Lookahead == '"')
                    {
                        sb.Append(_currentChar);
                        Advance();
                    }
                    else
                    {
                        done = true;
                    }

                    break;
                case null:
                    throw new LangFormatException("String is not closed", position.copy());
                default:
                    sb.Append(_currentChar);
                    Advance();
                    break;
            }
        }

        return new SyntaxToken
        {
            Name = "STRING",
            Symbol = sb.ToString(),
            Position = position.copy(),
            TokenType = TokenType.StringToken
        };
    }
}