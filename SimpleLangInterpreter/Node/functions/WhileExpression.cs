namespace SimpleLangInterpreter.Node;

public class WhileExpression : ExpresionSyntax
{
    public WhileExpression(SyntaxToken token) : base(token)
    {
    }

    public override IEnumerable<SyntaxNode> getChildren()
    {
        throw new NotImplementedException();
    }

    public override object execute()
    {
        throw new NotImplementedException();
    }
}