using SL.Parser.Models;
using SL.Parser.Models.Statements;
using SL.Tokenizer.Interfaces;

namespace SL.Parser.Handlers.Statements.Blocks;

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