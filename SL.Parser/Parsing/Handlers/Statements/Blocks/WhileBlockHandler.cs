using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Statements.Blocks;

public class WhileBlockHandler : IParserHandler<WhileBlockStatement>
{
    public async Task<WhileBlockStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {

        if (tokenIterator.LookUp().Value == "do")
        {
            return await HandleDoWhile(tokenIterator, parserFactory);
        }
        
        return await HandleWhile(tokenIterator, parserFactory);
    }


    private async Task<WhileBlockStatement> HandleDoWhile(ITokenIterator tokenIterator, NodeFactory parserFactory)
    {
        tokenIterator.NextToken("do");
        var body = await parserFactory.CreateNode<Statement>();

        tokenIterator.NextToken("while");
        var condition = await parserFactory.CreateNode<ParentasisExpressionStatement>();

        return new WhileBlockStatement(condition,body, true);
    }

    private async Task<WhileBlockStatement> HandleWhile(ITokenIterator tokenIterator, NodeFactory parserFactory)
    {
        tokenIterator.NextToken("while");
        var condition = await parserFactory.CreateNode<ParentasisExpressionStatement>();
        var body = await parserFactory.CreateNode<Statement>();
        return new WhileBlockStatement(condition,body, false);
    }
}