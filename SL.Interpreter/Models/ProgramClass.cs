namespace SL.Interpreter.Models;

public class ProgramClass
{
    private ProgramContext programContext;

    public string Name { get; set; }
    
    public Dictionary<string, ProgramVariable> Fields { get; set; }

    public Dictionary<string, ProgramFunction> Functions { get; set; }
    
    public Dictionary<string, ProgramFunction> Consturctors { get; set; }
    
    public ProgramObject NewInstance(params object[] argumetns)
    {
        return null;
    }


}