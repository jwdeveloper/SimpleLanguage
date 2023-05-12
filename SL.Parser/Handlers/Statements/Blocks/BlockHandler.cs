using SL.Parser.Models;
using SL.Parser.Models.Statements.Blocks;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements.Blocks;

public class BlockHandler : IParserHandler<BlockStatement>
{
    public async Task<BlockStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory,
        object[] parameters)
    {
        tokenIterator.NextToken(TokenType.OPEN_BLOCK);

        var nextToken = tokenIterator.LookUp();
        var body = nextToken.Type != TokenType.CLOSE_BLOCK
            ? await parserFactory.CreateNode<List<Statement>>(TokenType.CLOSE_BLOCK)
            : new List<Statement>();

        tokenIterator.NextToken(TokenType.CLOSE_BLOCK);
        return new BlockStatement(body);
    }
}