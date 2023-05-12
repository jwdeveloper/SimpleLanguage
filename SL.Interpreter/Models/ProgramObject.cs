namespace SL.Interpreter.Models;

public class ProgramObject
{
    private ProgramClass objectType;

    private Dictionary<string, ProgramVariable> Fields;

    private Dictionary<string, ProgramFunction> Functions;


    public ProgramClass GetType()
    {
        return objectType;
    }
}