using SL.Parser.Api;
using SL.Parser.Parsing.AST;

namespace SL.Parser.Parsing;

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