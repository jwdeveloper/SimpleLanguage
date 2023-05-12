using SL.Interpreter.Models;
using SL.Parser.Models;

namespace SL.Interpreter.Interpreters.Statements;

public class StatementInterpreter : IInterpreter<Statement>
{
    public Task<object> Interpreter(Statement node, ProgramContext program, InterpreterFactory factory)
    {
        return factory.InterpreterNode(node);
    }
}



