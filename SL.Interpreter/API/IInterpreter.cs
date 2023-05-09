using SL.Core.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public interface IInterpreter<T> where T : Node
{
    public  Task<object> Interpreter(T node, ProgramContext program, InterpreterFactory factory);
}