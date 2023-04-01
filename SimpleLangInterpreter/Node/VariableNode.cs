namespace SimpleLangInterpreter.Node;

public class VariableNode: ExpresionSyntax
{
    public VariableNode(SyntaxToken token) : base(token)
    {
    }
   
    public override object execute()
    {
       return evaluator.GetVariableValue(token.Symbol);
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
        return NodeType.VariableNode;
    }
}