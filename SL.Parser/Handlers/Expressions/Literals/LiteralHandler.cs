using System.Globalization;
using SL.Parser.Models;
using SL.Parser.Models.Literals;
using SL.Tokenizer.Exceptions;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Expressions.Literals;

public class LiteralHandler : IParserHandler<Literal>
{
    public async Task<Literal> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        var token = tokenIterator.NextToken();
        switch (token.Type)
        {
            case TokenType.BOOL:
                return new BoolLiteral(bool.Parse(token.Value));
            case TokenType.STRING:
                return new TextLiteral(token.Value);
            case TokenType.NUMBER:
                return new NumericLiteral(float.Parse(token.Value, CultureInfo.InvariantCulture));
            default:
                throw new SyntaxException("Unexpected literal", token);
        }
    }
}