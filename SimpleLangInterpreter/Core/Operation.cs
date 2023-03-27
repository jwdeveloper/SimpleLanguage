namespace SimpleLangInterpreter.Core;

public class Operation
{
    public string Name { get; set; }

    public List<SyntaxToken> Arguments { get; set; } = new List<SyntaxToken>();

    public List<Operation> Operations { get; set; } = new List<Operation>();
     
    public override string ToString()
    {
        var res = Name + ": ";
        foreach (var argument in Arguments)
        {
            res += argument.Symbol + " ";
        }

        res += " SubOperations: " + Operations.Count;
        return res;
    }
}