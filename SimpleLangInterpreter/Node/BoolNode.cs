namespace SimpleLangInterpreter.Node;

public class BoolNode : ExpresionSyntax
{
    public BoolNode(SyntaxToken token) : base(token)
    {
    }
   
    public override object execute()
    {
        if (token.Symbol == "true")
        {
            return true;
        }
        if (token.Symbol == "false")
        {
            return false;
        }
        return false;
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
        return NodeType.BoolNode;
    }
}