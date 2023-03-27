namespace SimpleLangInterpreter.Node;

public class Program : SyntaxNode
{
    public List<SyntaxNode> Nodes { get; set; } = new List<SyntaxNode>();

    public IEnumerable<SyntaxNode> getChildren()
    {
        return Nodes;
    }

    public NodeType getNoteType()
    {
        return NodeType.Program;;
    }

    public string getValue()
    {
        return "Program";
    }


    public virtual string Print()
    {
        return SyntaxNode.PrettyPrint(this,"",false);
    }
}