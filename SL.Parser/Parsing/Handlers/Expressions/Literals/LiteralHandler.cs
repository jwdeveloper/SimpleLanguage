using System.Globalization;
using SL.Parser.Api;
using SL.Parser.Api.Exceptions;
using SL.Parser.Common;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Expressions.Literals;

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