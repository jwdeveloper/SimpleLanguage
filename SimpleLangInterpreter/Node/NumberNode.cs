namespace SimpleLangInterpreter.Node;

public class NumberNode : ExpresionSyntax
{
    public NumberNode(SyntaxToken token) : base(token)
    {
        
    }


    public override string execute()
    {
        return token.Symbol;
    }

    public override string getValue()
    {
        return token.Symbol;
    }
    
    public override IEnumerable<SyntaxNode> getChildren()
    {
       return Enumerable.Empty<SyntaxNode>();
    }

    public override NodeType getNoteType()
    {
        return NodeType.NumberNode;
    }
}