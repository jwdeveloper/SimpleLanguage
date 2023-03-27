using System.Text;
using SimpleLangInterpreter.Node;

namespace SimpleLangInterpreter.Evaliating;

public class Evaluator
{
    public string runProgram(Program program)
    {
        var output = new StringBuilder();



        foreach (var current in program.getChildren())
        {
            if (current is BinaryExpression ex)
            {
                output.AppendLine(ex.execute());
            }
            
        }

        return output.ToString();
    }
}