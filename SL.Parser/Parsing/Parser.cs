using System.Linq.Expressions;
using SL.Parser.Api;
using SL.Parser.Api.Exceptions;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;
using SL.Parser.Parsing.AST.Statements;
using Expression = SL.Parser.Parsing.AST.Expression;

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