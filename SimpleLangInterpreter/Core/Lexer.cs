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

    public Lexer(string fileName, string input)
    {
        _fileName = fileName;
        _input = input.ToCharArray();
        position = new Position(-1, 0, -1, fileName, input);
        _defaultTokens = DefaultToken.GetDefaultTokens();
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
        var startLitteralPosition = position.copy();
        while (_currentChar != null)
        {
            if(LookSingleChar(out SyntaxToken token))
            {
                if (litteral != string.Empty)
                {
                    var literralTokens = ReadLitteral2(litteral,startLitteralPosition);
                    litteral = string.Empty;
                    startLitteralPosition = position.copy();
                    tokens.AddRange(literralTokens);
                }
                tokens.Add(token);
            }
            else
            {
                litteral += _currentChar;
            }

            Advance();
            if (_currentChar == null && litteral != string.Empty)
            {
                tokens.AddRange(ReadLitteral2(litteral,position));
            }
        }

        return tokens;
    }

    public bool LookSingleChar(out SyntaxToken token)
    {
        token = null;
        foreach (var defaultToken in _defaultTokens)
        {
            if (defaultToken.Symbol != _currentChar)
            {
                continue;
            }

            if (defaultToken == DefaultToken.QUOTE)
            {
                token = ReadString();
                return true;
            }

            if (_currentChar == DefaultToken.DIVIDE.Symbol && Lookahead == DefaultToken.DIVIDE.Symbol)
            {
                token = ReadComment();
                return true;
            }

            if (_currentChar == DefaultToken.DIVIDE.Symbol && Lookahead == '*')
            {
                token = ReadLongComment();
                return true;
            }

            token = new SyntaxToken()
            {
                Name = defaultToken.Name,
                Symbol = defaultToken.Symbol.ToString(),
                TokenType = defaultToken.TokenType,
                Position = position.copy(),
            };
            return true;
        }
        return false;
    }

    
    public List<SyntaxToken> ReadLitteral2(string currentText, Position position)
    {
        var result = new List<SyntaxToken>();
        var chars = currentText.ToCharArray();
        var world = string.Empty;
        foreach (var value in chars)
        {
            if (char.IsWhiteSpace(value))
            {
                if (world != string.Empty)
                {
                    if (isNumber(world))
                    {
                        result.Add(new SyntaxToken
                        {
                            Name = "NUMBER:INT",
                            Symbol = world,
                            TokenType = TokenType.NumberToken,
                            Position = position
                        });
                    }
                    else
                    {
                        result.Add(new SyntaxToken
                        {
                            Name = "LITTERAL",
                            Symbol = world,
                            TokenType = TokenType.LitteralToken,
                            Position = position
                        });  
                    }
                }
                result.Add(new SyntaxToken
                {
                    Name = "WHITESPACE",
                    Symbol = value.ToString(),
                    TokenType = TokenType.WhiteSpace,
                    Position = position
                });
                world = string.Empty;
            }
            else
            {
                world += value;
            }
        }
        if (world != string.Empty)
        {
            if (isNumber(world))
            {
                result.Add(new SyntaxToken
                {
                    Name = "NUMBER:INT",
                    Symbol = world,
                    TokenType = TokenType.NumberToken,
                    Position = position
                });
            }
            else
            {
                result.Add(new SyntaxToken
                {
                    Name = "LITTERAL",
                    Symbol = world,
                    TokenType = TokenType.LitteralToken,
                    Position = position
                });  
            }
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
        var startPosition = position.copy();
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
            Symbol = "//"+comment.ToString(),
            Position = startPosition,
            TokenType = TokenType.Comment
        };
    }

    private SyntaxToken ReadLongComment()
    {
        var startPosition = position.copy();
        var comment = new StringBuilder();
        Advance();
        Advance();
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
            Symbol = "/*"+comment.ToString()+"*/",
            Position = startPosition,
            TokenType = TokenType.Comment
        };
    }


    private SyntaxToken ReadString()
    {
        Advance();
        var sb = new StringBuilder();
        var done = false;
        var startPosition = position.copy();
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
            Position = startPosition,
            TokenType = TokenType.StringToken
        };
    }
}