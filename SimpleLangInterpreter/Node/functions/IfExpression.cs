namespace SimpleLangInterpreter.Node;

public class IfExpression : ExpresionSyntax
{
    private readonly ExpresionSyntax arguments;
    private readonly ExpresionSyntax body;
    private readonly ExpresionSyntax elseBody;

    public IfExpression(SyntaxToken token, ExpresionSyntax arguments, ExpresionSyntax body,
        ExpresionSyntax elseBody) : base(token)
    {
        this.arguments = arguments;
        this.body = body;
        this.elseBody = elseBody;
    }


    public override string getValue()
    {
        return "IF-statement";
    }

    public override IEnumerable<SyntaxNode> getChildren()
    {
        if (elseBody == null)
        {
            return new List<SyntaxNode>()
            {
                arguments, body
            }; 
        }
        
        
        return new List<SyntaxNode>()
        {
            arguments, body, elseBody
        };
    }

    public override object execute()
    {
        var result = (bool) arguments.execute();
        if (result)
        {
            body.execute();
        }
        else 
        {
            if (elseBody == null)
            {
                return result;
            }
            
            elseBody.execute();
        }
        return result;
    }
}