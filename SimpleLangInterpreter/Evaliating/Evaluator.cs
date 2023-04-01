using System.Text;
using SimpleLangInterpreter.Node;

namespace SimpleLangInterpreter.Evaliating;

public class Evaluator
{
    StringBuilder output = new StringBuilder();
    private Dictionary<string, object> variables;

    public string runProgram(Program program)
    {
        output = new StringBuilder();
        variables = new Dictionary<string, object>();
        foreach (var current in program.getChildren())
        {
            if (current is ExpresionSyntax ex)
            {
                ex.setEvaluator(this);
                ex.execute();
                // output.AppendLine(ex.execute().ToString());
            }
        }

        return output.ToString();
    }

    public void CallConsole(string input)
    {
        output.Append(input);
    }


    public bool CreateVariable(string name,object value)
    {
        if (variables.ContainsKey(name))
        {
            throw new Exception($"Variable {name} already declared");
        }

       // CallConsole($"Variable create {name} with value {value}");
        variables[name] = value;
        return true;
    }
    
    public object GetVariableValue(string name)
    {
        if (!variables.ContainsKey(name))
        {
            throw new Exception($"Variable {name} not exists declared");
        }

       return variables[name];
    }
    
    public bool SetVariableValue(string name,object value)
    {
        if (!variables.ContainsKey(name))
        {
            throw new Exception($"Variable {name} not exists declared");
        }
      //  CallConsole($"Variable {name} value changed from {variables[name]} to {value}");
        variables[name] = value;
        return true;
    }
}