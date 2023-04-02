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
                if (val is List<object> list)
                {
                    res += "[";
                    foreach(var value in list)
                    {
                        res += value+ ", ";
                    }
                    res += "] ";
                    continue;
                }
                
                
                if (val  is string  str && str.Contains("/n"))
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

        if (name == "sleep")
        {
            var argument = arguments[0];
            var time = (double)argument.execute();
            var timeInt = (int)time;
            Thread.Sleep(timeInt);
            return true;
        }
        if (name == "clear")
        {
            evaluator.ClearConsole();
            return true;
        }
        if (name == "random")
        {
            var l1 = (double)arguments[0].execute();
            var l2 = (double)arguments[1].execute();

            var arg1 = (int)l1;
            var arg2 = (int)l2;
            
            var random = new Random();
            return random.Next(arg1, arg2);
        }
        
        if (name == "range")
        {
            var list = new List<object>();
            var l1 = (double)arguments[0].execute();
            var l2 = (double)arguments[1].execute();
            var arg1 = (int)l1;
            var arg2 = (int)l2;
            for (var i = arg1; i <= arg2; i++)
            {
                list.Add(i);
            }
            return list;
        }
        return new NotImplementedException($"Function {name} not implremented yet");
    }
}