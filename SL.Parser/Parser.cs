using SL.Parser.Models;
using SL.Tokenizer.Interfaces;

namespace SL.Parser;

public class Parser
{
    private readonly NodeFactory _nodeFactory;
    private ITokenIterator? _tokenIterator;

    public Parser(NodeFactory nodeFactory)

    {
        _nodeFactory = nodeFactory;
        _tokenIterator = null;
    }

    public async Task<SlProgram> ParseNew()
    {
        return await _nodeFactory.CreateNode<SlProgram>();
    }
  
}