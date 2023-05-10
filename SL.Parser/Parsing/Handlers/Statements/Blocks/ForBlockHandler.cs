using SL.Parser.Api;
using SL.Parser.Api.Exceptions;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Statements;

namespace SL.Parser.Parsing.Handlers.Statements.Blocks;

public class ForBlockHandler : IParserHandler<ForLoopStatement>
{
    public async Task<ForLoopStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        tokenIterator.NextToken("for");
        tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);

        var declaration = tokenIterator.LookUp().Type == TokenType.END_OF_LINE
            ? null
            : await  parserFactory.CreateNode<VariableStatement>();
        if (tokenIterator.LookUp().Value == "in")
        {
            return await HandleForeach(tokenIterator, parserFactory, declaration);
        }

        tokenIterator.NextToken(TokenType.END_OF_LINE);

        var condition = tokenIterator.LookUp().Type == TokenType.END_OF_LINE ? null :  await parserFactory.CreateNode<Expression>();
        tokenIterator.NextToken(TokenType.END_OF_LINE);

        var assigment = tokenIterator.LookUp().Type == TokenType.CLOSE_ARGUMENTS ? null :  await parserFactory.CreateNode<Expression>();
        tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);

        var body = await parserFactory.CreateNode<Statement>();
 

        return new ForStatement(declaration, condition, assigment, body);
    }

    private async Task<ForLoopStatement> HandleForeach(ITokenIterator tokenIterator, 
        NodeFactory parserFactory,
        VariableStatement? declaration)
    {
        if (declaration is null)
        {
            throw new SyntaxException("Foreach must have variable declaration", tokenIterator.CurrentToken());
        }

        tokenIterator.NextToken("in");

        var iterator = await parserFactory.CreateNode<Expression>();
        tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);

        var body = await parserFactory.CreateNode<Statement>();
        return new ForeachStatement(declaration, iterator, body);
    }
}