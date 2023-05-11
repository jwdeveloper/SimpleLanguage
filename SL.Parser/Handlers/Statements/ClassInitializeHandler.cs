using SL.Parser.Api;
using SL.Parser.Api.Exceptions;
using SL.Parser.Common;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Statements;

public class ClassInitializeHandler : IParserHandler<ClassInitializeExpressionStatement>
{
    public async Task<ClassInitializeExpressionStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        tokenIterator.NextToken("new", TokenType.KEYWORLD);
        var initializeCall = await parserFactory.CreateNode<IdentifierLiteral>();
        if (initializeCall is  FunctionCallExpression expression)
        {
            return new ClassInitializeExpressionStatement(expression);
        }
        throw new SyntaxException("bad class initiazation", tokenIterator.CurrentToken());
    }
}