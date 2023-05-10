using SL.Parser.Parsing.AST.Statements;

namespace SL.Interpreter.Interpreters;

public class BreakInterpreter : IInterpreter<BreakStatement>
{
    public async Task<object> Interpreter(BreakStatement node, ProgramContext program, InterpreterFactory factory)
    {
        return new BreakOperation();
    }
}