namespace SL.Interpreter.Models;

public class ProgramContext
{

    public CancellationToken CANCELLATION_TOKEN;
    public  Dictionary<string, ProgramVariable> Variables {get;}
    public  Dictionary<string, ProgramFunction> Functions { get; }
    public  Dictionary<string, ProgramClass> Classes { get; }
    public  List<string> Console { get; }
    
    private readonly List<string> _progamTypes;
    public Action<List<string>> onConsoleUpdate { get; set; }

    private readonly Stack<ProgramVariable> _variablesStack;


    public ProgramContext(CancellationToken cancellationToken)
    {
        CANCELLATION_TOKEN = cancellationToken;
        Variables = new Dictionary<string, ProgramVariable>();
        Functions = new Dictionary<string, ProgramFunction>();
        Classes = new Dictionary<string, ProgramClass>();
        Console = new List<string>();
        _progamTypes = new List<string>();
        _variablesStack = new Stack<ProgramVariable>();
    }

    public bool IsCancelRequested 
    {
        get
        {
            if (CANCELLATION_TOKEN.IsCancellationRequested)
            {
                throw new Exception("End of program");
            }
            return CANCELLATION_TOKEN.IsCancellationRequested;
        }
    }

    
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


    public bool CreateProgramClass(ProgramClass programClass)
    {
        if (IsClassExists(programClass.Name))
        {
            throw new Exception($"Class type {programClass.Name} already declared");
        }
        Classes.Add(programClass.Name, programClass);
        return true;
    }
    
    public ProgramClass GetClassType(string name)
    {
        if (!IsClassExists(name))
        {
            throw new Exception($"Class type {name} is not declared");
        }

        return Classes[name];
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
    
    public ProgramFunction GetFunction(string name)
    {
        if (!IsFunctionExists(name))
        {
            throw new Exception($"Function {name} is not declared");
        }

        return Functions[name];
    }

    public object GetVariableValue(object value, string type)
    {
        if (value is ProgramVariable programVariable)
        {
            return GetVariableValue(programVariable.Value, type);
        }

        if (!IsValueMatchType(type, value))
        {
            throw new Exception($"Required type for variable {value} is {type}");
        }
        return value;
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
    
    public bool IsClassExists(string className)
    {
        return Classes.ContainsKey(className);
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
        CreateVariable(new ProgramVariable() { Type = type, Name = name, Value = value });
    }
    
    public void RemoveVariable(int count= 1)
    {
        while (count > 0)
        {
           var variable = _variablesStack.Pop();
           if (variable == null)
           {
               break;
           }
           Variables.Remove(variable.Name);
           count--;
        }
    }
    
    public void CreateVariable(ProgramVariable programVariable)
    {
        if (Variables.ContainsKey(programVariable.Name))
        {
            throw new Exception($"Variable already declared {programVariable.Name}");
        }
        Variables.Add(programVariable.Name, programVariable);
        _variablesStack.Push(programVariable);
    }
    
    public ProgramVariable GetVariable(string name)
    {
        if (!Variables.ContainsKey(name))
        {
            throw new Exception("Variable not found");
        }
        return Variables[name];
    }


    public string FindObjectType(object value)
    {
        if (value is null)
        {
            return "null";
        }
        
        if (value is float)
        {
            return  "number";
        }
        if (value is bool)
        {
            return  "bool";
        }
        if (value is string)
        {
            return  "text";
        }
        return "var";
    }
    
    public bool IsValueMatchType(string type, object value)
    {
        if (value is ProgramVariable variable)
        {
            return IsValueMatchType(type, variable.Value);
        }
        
        if (type == "number" )
        {
            if (value is float or int or double)
            {
                return true;
            }
            return false;
        }
        if (type == "bool")
        {
            return value is bool;
        }
        if (type == "text")
        {
            return value is string;
        }
        if (type == "list")
        {
            return value is List<object>;
        }
        
        if (type == "null")
        {
            return value is null;
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
            return IsBoolean(variable.Value);
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
            return GetBoolValue(variable.Value);
        }

        return target != null;
    }


}