using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Expressions;

public class ExpressionBinaryHandler : IParserHandler<Expression>
{
    public async Task<Expression> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        var finder = new ExpressionFinder(tokenIterator, parserFactory)
            .ThenByType(TokenType.LOGICAL_OPERATOR)
            .ThenByType(TokenType.EQUALITY_OPREATOR)
            .ThenByType(TokenType.BINARY_ADDATIVE_OPERATOR)
            .ThenByType(TokenType.BINARY_MULTIPLICATION_OPERATOR)
            .Then(async () =>
            {
                var token = tokenIterator.LookUp();
                switch (token.Type)
                {
                    case TokenType.OPEN_ARGUMETNS:
                        return await parserFactory.CreateNode<ParentasisExpressionStatement>();
                    case TokenType.IDENTIFIER:
                    case TokenType.KEYWORLD:
                        if (token.Value == "new")
                            return await parserFactory.CreateNode<ClassInitializeExpressionStatement>();
                        else
                        return await parserFactory.CreateNode<IdentifierLiteral>();
                    default:
                        return await parserFactory.CreateNode<Literal>();
                }
            });
        return await finder.Find();
    }


    public class ExpressionFinder
    {
        private ITokenIterator _tokenIterator;
        private NodeFactory _parserFactory;
        private List<TokenType> tokenTypes;
        private Func<Task<Expression>> then;

        public ExpressionFinder(ITokenIterator tokenIterator, NodeFactory parserFactory)
        {
            _tokenIterator = tokenIterator;
            _parserFactory = parserFactory;
            tokenTypes = new List<TokenType>();
        }

        public ExpressionFinder ThenByType(TokenType type)
        {
            tokenTypes.Add(type);
            return this;
        }

        public ExpressionFinder Then(Func<Task<Expression>> then)
        {
            this.then = then;
            return this;
        }

        private async Task<Expression> GetGenericExpression(TokenType tokenType, Func<Task<Expression>> next)
        {
            var left = await next.Invoke();
            while (_tokenIterator.LookUp().Type == tokenType)
            {
                var binaryOperator = _tokenIterator.NextToken(tokenType);
                var right = await next.Invoke();

                left = new BinaryExpression(binaryOperator, left, right);
            }

            return left;
        }

        public async Task<Expression> Find()
        {
            var f1 = () => GetGenericExpression(TokenType.BINARY_MULTIPLICATION_OPERATOR, then);
            var f2 = () => GetGenericExpression(TokenType.BINARY_ADDATIVE_OPERATOR, f1);
            var f3 = () => GetGenericExpression(TokenType.EQUALITY_OPREATOR, f2);
            var f4 = () => GetGenericExpression(TokenType.LOGICAL_OPERATOR, f3);

            return await f4.Invoke();
        }
    }
}