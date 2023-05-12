using SL.Interpreter.Models;
using SL.Parser.Models.Statements;

namespace SL.Interpreter.Interpreters.Statements;

public class BreakInterpreter : IInterpreter<BreakStatement>
{
    public async Task<object> Interpreter(BreakStatement node, ProgramContext program, InterpreterFactory factory)
    {
        return new BreakOperation();
    }
}