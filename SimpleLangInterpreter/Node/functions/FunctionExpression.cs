namespace SimpleLangInterpreter.Node;

public class FunctionExpression: ExpresionSyntax
{
    public FunctionExpression(SyntaxToken token,string name, List<ExpresionSyntax> arguments, ExpresionSyntax body) : base(token)
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