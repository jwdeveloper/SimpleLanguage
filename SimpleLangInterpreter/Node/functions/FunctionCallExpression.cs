namespace SimpleLangInterpreter.Node;

public class FunctionCallExpression: ExpresionSyntax
{
    private string name;
    private List<ExpresionSyntax> arguments;

    public FunctionCallExpression(SyntaxToken token,string name, List<ExpresionSyntax> arguments) : base(token)
    {
        this.name = name;
        this.arguments = arguments;
    }

    public override IEnumerable<SyntaxNode> getChildren()
    {
        return arguments;
    }

    public override string getValue()
    {
        return "Funcation call - " + name;
    }

    public override object execute()
    {
        if (name == "print")
        {
            var res = string.Empty;
            foreach (var argument in arguments)
            {

                var val = argument.execute();
                if (val  is string  str && str.Contains("/br"))
                {
                    res += "\n" ;
                }
                else
                {
                    res += val+ " ";
                }
                
                
                
            }

            if (evaluator != null)
            {
                evaluator.CallConsole(res);
            }
            return res;
        }

        return new NotImplementedException($"Function {name} not implremented yet");
    }
}