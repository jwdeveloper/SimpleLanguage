using SL.Parser.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public class DefaultInterpreter<T> : IInterpreter<T> where T : Node
{
    public delegate Task<object> DefaultAction<K>(K obj, ProgramContext program, InterpreterFactory factory);


    private DefaultAction<T> action;

    public DefaultInterpreter(DefaultAction<T> action)
    {
        this.action = action;
    }


    public async Task<object> Interpreter(T node, ProgramContext program, InterpreterFactory factory)
    {
        return await action.Invoke(node, program, factory);
    }
}