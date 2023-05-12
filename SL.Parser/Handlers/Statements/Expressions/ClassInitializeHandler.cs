using SL.Parser.Models.Expressions;
using SL.Parser.Models.Literals;
using SL.Tokenizer.Exceptions;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements.Expressions;

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