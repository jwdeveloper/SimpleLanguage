using SimpleLangInterpreter.Evaliating;

namespace SimpleLangInterpreter.Node;

public abstract  class ExpresionSyntax : SyntaxNode
{

    public readonly SyntaxToken token;
    protected Evaluator evaluator;


    public ExpresionSyntax(SyntaxToken token)
    {
        this.token = token;
    }

    public abstract IEnumerable<SyntaxNode> getChildren();
    public virtual NodeType getNoteType()
    {
        return  NodeType.Expression;
    }


    public void setEvaluator(Evaluator evaluator)
    {
        this.evaluator = evaluator;
        foreach(var child in getChildren())
        {
            if (child is ExpresionSyntax c)
            {
                c.setEvaluator(evaluator);
            }
        }
    }


    public abstract object execute();
    
    public virtual string getValue()
    {
        return "Expression";
    }
}