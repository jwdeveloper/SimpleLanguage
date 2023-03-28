namespace SimpleLangInterpreter.Node;

public class UndefindExpression : ExpresionSyntax
{
    private string message = "Undefind";
    public UndefindExpression(string message) : base(new SyntaxToken()
    {
        Name = "Undefind",
        TokenType = TokenType.Undefined
    })
    {
        this.message = message;
    }

    public override IEnumerable<SyntaxNode> getChildren()
    {
        return new List<SyntaxNode>() {token};
    }

    public override string getValue()
    {
        return message;
    }

    public override string execute()
    {
        return "";
    }
}