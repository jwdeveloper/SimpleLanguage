using System.Text;

namespace SimpleLangInterpreter.Node;

public interface SyntaxNode
{
    public IEnumerable<SyntaxNode> getChildren();

    public NodeType getNoteType();

    public string getValue();
    
    public  static string PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
    {
        var str = new StringBuilder();
        var marker = isLast ? "└──" : "├──";

        str.Append(indent);
        str.Append(marker);
        str.Append(node.getNoteType());

        str.Append(" ");
        str.Append(node.getValue());

        str.Append("\n");

        indent += isLast ? "    " : "│   ";

        var lastChild = node.getChildren().LastOrDefault();

        foreach (var child in node.getChildren())
        {
           var res = PrettyPrint(child, indent, child == lastChild);
           str.Append(res);
        }
        return str.ToString();
    }
}