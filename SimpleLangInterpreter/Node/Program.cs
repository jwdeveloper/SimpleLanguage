using SimpleLangInterpreter.Evaliating;

namespace SimpleLangInterpreter.Node;

public class Program : ExpresionSyntax
{
    
    public Program() : base(new SyntaxToken
    {
        Name = "Program",
        TokenType = TokenType.Undefined,
        Symbol = "",
    })
    {
    }
    
    public List<SyntaxNode> Nodes { get; set; } = new List<SyntaxNode>();


    public override IEnumerable<SyntaxNode> getChildren()
    {
        return Nodes;
    }

    public NodeType getNoteType()
    {
        return NodeType.Program;;
    }

    public override object execute()
    {
        var eva = new Evaluator(this.evaluator, this.evaluator.cancellationToken);
        eva.runProgram(this);
        return true;
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