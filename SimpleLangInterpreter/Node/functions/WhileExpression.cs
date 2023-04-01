namespace SimpleLangInterpreter.Node;

public class WhileExpression : ExpresionSyntax
{
    private readonly ExpresionSyntax arguments;
    private readonly ExpresionSyntax body;
    public WhileExpression(SyntaxToken token, ExpresionSyntax arguments, ExpresionSyntax body) : base(token)
    {
        this.arguments = arguments;
        this.body = body;
    }

    public override IEnumerable<SyntaxNode> getChildren()
    {
        return new List<SyntaxNode>()
        {
            arguments, body
        };
    }

    public override object execute()
    {
         var deepnes = 100;
         var result = isTrue();
         while (result && deepnes > 0)
         {
             body.execute();
             result = isTrue();
             deepnes--;
         }
         return result;
    }
    
    public bool isTrue()
    {
        var argument = arguments.execute();
        if (argument is bool arg)
        {
            return arg;
        }
        return argument != null;
    }
}