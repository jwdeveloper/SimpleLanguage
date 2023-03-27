namespace SimpleLangInterpreter.Node;

public abstract  class ExpresionSyntax : SyntaxNode
{

    protected readonly SyntaxToken token;


    public ExpresionSyntax(SyntaxToken token)
    {
        this.token = token;
    }

    public abstract IEnumerable<SyntaxNode> getChildren();
    public virtual NodeType getNoteType()
    {
        return  NodeType.Expression;
    }


    public abstract string execute();
    
    public virtual string getValue()
    {
        return "Expression";
    }
}