namespace SimpleLangInterpreter.Node;

public class UndefindExpression : ExpresionSyntax
{
    public UndefindExpression() : base(new SyntaxToken()
    {
        Name = "Undefind",
        TokenType = TokenType.Undefined
    })
    {
    }

    public override IEnumerable<SyntaxNode> getChildren()
    {
        return new List<SyntaxNode>() {token};
    }

    public override string execute()
    {
        return "";
    }
}