using System.Text;
using SimpleLangInterpreter.Node;

namespace SimpleLangInterpreter.Evaliating;

public class Evaluator
{
    StringBuilder output = new StringBuilder();
    private Dictionary<string, object> variables;

    private Action<string> onConsole;

    public Evaluator parent;

    public CancellationToken cancellationToken;

    public Evaluator(Evaluator parent, CancellationToken cancellationToken)
    {
        this.parent = parent;
        this.cancellationToken = cancellationToken;
    }

    public string runProgram(Program program)
    {
        output = new StringBuilder();
        variables = new Dictionary<string, object>();
        foreach (var current in program.getChildren())
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return output.ToString();
            }
            
            if (current is ExpresionSyntax ex)
            {
                ex.setEvaluator(this);
                ex.execute();
                // output.AppendLine(ex.execute().ToString());
            }
        }
        return output.ToString();
    }


    public void OnConsole(Action<string> console)
    {
        this.onConsole = console;
    }

    public void ClearConsole()
    {
        if (parent != null)
        {
            parent.ClearConsole();
            return;
        }
        
        output.Clear();
        CallConsole(string.Empty);
    }

    public string GetConsole()
    {
        if (parent != null)
        {
           return parent.GetConsole();
        }
        
        var current = output.ToString();
        output = new StringBuilder(current);
        
        return current;
    }
    
    public void CallConsole(string input)
    {
        if (parent != null)
        {
            parent.CallConsole(input);
            return;
        }
        output.Append(input);
        if (onConsole != null)
        {
            onConsole.Invoke(input);
        }

      
    }

    public bool isVariableDeclared(string name)
    {
        if (variables.ContainsKey(name))
        {
            return true;
        }

        if (parent != null && parent.isVariableDeclared(name))
        {
            return true;
        }

        return false;
    }


    public bool CreateVariable(string name, object value)
    {
        if (isVariableDeclared(name))
        {
            throw new Exception($"Variable {name} already declared");
        }

        // CallConsole($"Variable create {name} with value {value}");
        variables[name] = value;
        return true;
    }

    public object GetVariableValue(string name)
    {
        if (!isVariableDeclared(name))
        {
            throw new Exception($"Variable {name} not exists declared");
        }

        if (variables.ContainsKey(name))
        {
            return variables[name];
        }

        if (parent == null)
        {
            return null;
        }

        return parent.GetVariableValue(name);
    }

    public bool SetVariableValue(string name, object value)
    {
        if (!isVariableDeclared(name))
        {
            throw new Exception($"Variable {name} not exists declared");
        }

        //  CallConsole($"Variable {name} value changed from {variables[name]} to {value}");
        if (variables.ContainsKey(name))
        {
            variables[name] = value;
            return true;
        }

        if (parent == null)
        {
            return false;
        }

        return parent.SetVariableValue(name, value);
    }

    public bool DeleteVariable(string name)
    {
        if (!isVariableDeclared(name))
        {
            throw new Exception($"Variable {name} not exists declared");
        }

        //  CallConsole($"Variable {name} deleted}");
        if (parent == null)
        {
            variables.Remove(name);
            return true;
        }

        return parent.DeleteVariable(name);
    }
}