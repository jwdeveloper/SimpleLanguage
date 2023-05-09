namespace SL.Interpreter.Interpreters;

public class ProgramFunction
{
    public string? Type;
    public string Name;
    public string[] Parameters;
    public Func<object[],ProgramContext, Task<object>> Invoker;
}