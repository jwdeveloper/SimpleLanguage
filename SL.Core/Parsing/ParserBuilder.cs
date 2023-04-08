using SL.Core.Lexing;
using SL.Core.Parsing.AST;

namespace SL.Core.Parsing;

public class ParserBuilder
{
    public delegate Node RuleEvent(NodeFactory nodeFactory, List<Node> rightNodes);
    
    private readonly Lexer _lexer;

    public ParserBuilder(Lexer lexer)
    {
        _lexer = lexer;
    }

    public ParserBuilder WithRule(string rule, RuleEvent onAction)
    {
        return this;
    }
    
    
   

    public Parser Build()
    {
        return new Parser(_lexer, new Diagnostic());
    }
}