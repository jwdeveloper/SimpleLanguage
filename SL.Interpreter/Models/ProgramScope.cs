namespace SL.Interpreter.Interpreters;

public class ProgramScope
{
    private ProgramScope ParentScope;
    
    private Dictionary<string, ProgramFunction> Functions { get; set; }
    
    private List<ProgramObject> Objects { get; }
    
    private Dictionary<string, ProgramVariable> Variables {get;}
    
    private readonly Stack<ProgramVariable> _variablesStack;


    public bool CreateObject()
    {
        return false;
    }
    
    public bool CreateVariable(ProgramVariable programVariable)
    {
        return false;
    }

    public void RemoveVariable(int count = 1)
    {
        while (count > 0)
        {
            var variable = _variablesStack.Pop();
            Variables.Remove(variable.Name);
            count--;
        }
    }
    
 
    
    
  
}