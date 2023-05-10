using SL.Parser.Api;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Statements.Blocks;

public class IfBlockHandler : IParserHandler<IfBlockStatement>
{
    public async Task<IfBlockStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {
        tokenIterator.NextToken("if");
        var condition = await parserFactory.CreateNode<ParentasisExpressionStatement>();
        var body = await parserFactory.CreateNode<Statement>();
        if (tokenIterator.LookUp().Value != "else")
        {
            return new IfBlockStatement(condition, body, null);
        }

        tokenIterator.NextToken("else");
        if (tokenIterator.LookUp().Value == "if")
        {
            var nestedIfBlock = await parserFactory.CreateNode<IfBlockStatement>();
            return new IfBlockStatement(condition, body, nestedIfBlock);
        }

        var elseStatement = await parserFactory.CreateNode<Statement>();
        return new IfBlockStatement(condition, body, elseStatement);
    }
}