namespace SimpleLangInterpreter.Node;

public class ForExpression : ExpresionSyntax
{
    private List<SyntaxNode> arguments;
    
    public ExpresionSyntax body;
    public ForExpression(SyntaxToken token, List<SyntaxNode> arguments, ExpresionSyntax body) : base(token)
    {
        this.arguments = arguments;
        this.body = body;
    }

    public override IEnumerable<SyntaxNode> getChildren()
    {
        var list = new List<SyntaxNode>();
        list.AddRange(arguments);
        list.Add(body);
        return list;
    }

    public override object execute()
    {
        if (arguments.Count != 3)
        {
            throw new Exception("Bad Implementation of for");
        }
        
        var declare = (ExpresionSyntax)arguments[0];
        var condition = (ExpresionSyntax)arguments[1];
        var incerase = (ExpresionSyntax)arguments[2];
        var deepnes = 100;

        declare.execute();
        while (isConditionTrue(condition) || deepnes < 0)
        {
            body.execute();
            incerase.execute();
            deepnes--;
        }
        return true;
    }
    
    
    public bool isConditionTrue(ExpresionSyntax condition)
    {
        var argument = condition.execute();
        if (argument is bool arg)
        {
            return arg;
        }
        return argument != null;
    }
}