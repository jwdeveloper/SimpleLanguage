using SL.Interpreter.Models;
using SL.Parser.Models.Statements;

namespace SL.Interpreter.Interpreters.Statements;

public class ExpresionStatementInterpreter : IInterpreter<ExpresionStatement>
{
    public async Task<object> Interpreter(ExpresionStatement node, ProgramContext program, InterpreterFactory factory)
    {
        return await factory.InterpreterNode(node.Expression);
    }
}