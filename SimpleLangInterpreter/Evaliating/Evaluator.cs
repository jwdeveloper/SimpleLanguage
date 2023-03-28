using System.Text;
using SimpleLangInterpreter.Node;

namespace SimpleLangInterpreter.Evaliating;

public class Evaluator
{
    StringBuilder output = new StringBuilder();

    public string runProgram(Program program)
    {
        output = new StringBuilder();
        foreach (var current in program.getChildren())
        {
            if (current is ExpresionSyntax ex)
            {
                ex.setEvaluator(this);
                output.AppendLine(ex.execute().ToString());
            }
        }

        return output.ToString();
    }

    public void CallConsole(string input)
    {
        output.Append(input);
    }
}