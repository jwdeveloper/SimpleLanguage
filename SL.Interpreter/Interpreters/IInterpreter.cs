using SL.Interpreter.Models;

namespace SL.Interpreter.Interpreters;

public interface IInterpreter<T>
{
    public  Task<object> Interpreter(T node, ProgramContext program, InterpreterFactory factory);
}