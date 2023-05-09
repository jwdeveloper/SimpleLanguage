using SL.Interpreter.Interpreters;

namespace SL.Interpreter;

public class ProgramContext
{

    public CancellationToken CANCELLATION_TOKEN;
    public  Dictionary<string, ProgramVariable> Variables {get;}
    public  Dictionary<string, ProgramFunction> Functions { get; }
    public  List<string> Console { get; }
    
    private readonly List<string> _progamTypes;
    public Action<List<string>> onConsoleUpdate { get; set; }


    public ProgramContext(CancellationToken cancellationToken)
    {
        CANCELLATION_TOKEN = cancellationToken;
        Variables = new Dictionary<string, ProgramVariable>();
        Functions = new Dictionary<string, ProgramFunction>();
        Console = new List<string>();
        _progamTypes = new List<string>();
    }

    public bool IsCancelRequested => CANCELLATION_TOKEN.IsCancellationRequested;

    
    public void AddConsoleOutput(string output)
    {
        Console.Add(output);
        if (onConsoleUpdate != null)
            onConsoleUpdate.Invoke(Console);
    }
    
    public void ClearConsole()
    {
        Console.Clear();
        if (onConsoleUpdate != null)
            onConsoleUpdate.Invoke(Console);
    }
    
    
    public bool CreateFunction(ProgramFunction programFunction)
    {
        if (IsFunctionExists(programFunction.Name))
        {
            throw new Exception($"Function {programFunction.Name} already declared");
        }
        Functions.Add(programFunction.Name, programFunction);
        return true;
    }

    public async Task<object> InvokeFunction(string functionName, params object[] param)
    {
        if (!IsFunctionExists(functionName))
        {
            throw new Exception($"Function {functionName} is not declared");
        }
        var function = Functions[functionName];

        if (function.Invoker == null)
        {
            return null;
        }
        
        return await function.Invoker.Invoke(param, this);
    }
    
    public bool IsFunctionExists(string functionName)
    {
        return Functions.ContainsKey(functionName);
    }


    public bool IsTypeExists(string typeName)
    {
        return _progamTypes.Contains(typeName);
    }
    
  
    public bool IsVariableExists(string variableName)
    {
        return Variables.ContainsKey(variableName);
    }

    public void CreateVariable(string type, string name, object value)
    {
        CreateVariable(new ProgramVariable() { type = type, name = name, value = value });
    }
    
    public void RemoveVariable(string name)
    {
        if (!IsVariableExists(name))
        {
            throw new Exception($"Variable not declared  {name}");
        }
        Variables.Remove(name);
    }
    
    public void CreateVariable(ProgramVariable programVariable)
    {
        if (Variables.ContainsKey(programVariable.name))
        {
            throw new Exception($"Variable already declared {programVariable.name}");
        }
        Variables.Add(programVariable.name, programVariable);
    }
    
    public ProgramVariable GetVariable(string name)
    {
        if (!Variables.ContainsKey(name))
        {
            throw new Exception("Variable not found");
        }
        return Variables[name];
    }
    
    
    public bool IsValueMatchType(string type, object value)
    {
        if (type == "number" )
        {
            return  value is float;
        }
        if (type == "bool")
        {
            return value is bool;
        }
        if (type == "text")
        {
            return value is string;
        }

        if (type == "var")
        {
            return true;
        }
        
        return false;
    }
    
    public bool IsBoolean(object target)
    {
        if (target is bool)
        {
            return true;
        }

        if (target is ProgramVariable variable)
        {
            return IsBoolean(variable.value);
        }

        return target != null;
    }
    
    public bool GetBoolValue(object target)
    {
        if (target is bool boolValue)
        {
            return boolValue;
        }

        if (target is ProgramVariable variable)
        {
            return GetBoolValue(variable.value);
        }

        return target != null;
    }


}