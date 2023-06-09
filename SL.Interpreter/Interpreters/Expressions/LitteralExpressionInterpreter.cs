using SL.Interpreter.Models;
using SL.Parser.Models;

namespace SL.Interpreter.Interpreters.Expressions;

public class LitteralExpressionInterpreter<T> : IInterpreter<T> where T : Literal
{
    public async Task<object> Interpreter(T node, ProgramContext program, InterpreterFactory factory)
    {
        return node.Value;
    }
}