namespace SimpleLangInterpreter.Node;

public class ForEachExpression : ForExpression
{
    private InExpression inExpression;
    
    public ForEachExpression(SyntaxToken token, InExpression inExpression , ExpresionSyntax body) : base(token, new List<SyntaxNode>(), body)
    {
        this.inExpression = inExpression;
    }

    public override IEnumerable<SyntaxNode> getChildren()
    {
        return new List<SyntaxNode>()
        {
            inExpression, this.body
        };
    }

    public override object execute()
    {
        var rightValue = inExpression.RightNode().execute();
        if (rightValue is not List<object> list)
        {
            throw new Exception("List in foreach must be iterable");
        }
        inExpression.LeftNode().execute();
        var variableName = inExpression.LeftNode().VariableName.Symbol;
        for (var i = 0; i < list.Count; i++)
        {
            var value = list[i];
            evaluator.SetVariableValue(variableName, value);
            body.execute();
        }
        return true;
    }
}