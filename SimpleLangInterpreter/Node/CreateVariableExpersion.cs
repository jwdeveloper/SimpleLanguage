namespace SimpleLangInterpreter.Node;

public class CreateVariableExpersion : ExpresionSyntax
{
    public SyntaxToken VariableName;
    public SyntaxToken VariableType;
    
    public CreateVariableExpersion(SyntaxToken token, SyntaxToken varableName) : base(token)
    {
        this.VariableName = varableName;
        this.VariableType = token;
    }

    public override IEnumerable<SyntaxNode> getChildren()
    {
        return new[]
        {
            VariableType, VariableName
        };
    }

    public override object execute()
    {
        evaluator.CreateVariable(VariableName.Symbol, null);
        return true;
    }
}