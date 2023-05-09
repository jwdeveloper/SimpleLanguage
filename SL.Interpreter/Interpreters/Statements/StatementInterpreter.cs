using SL.Core.Parsing.AST;
using SL.Core.Parsing.AST.Expressions;

namespace SL.Interpreter.Interpreters;

public class StatementInterpreter : IInterpreter<Statement>
{
    public Task<object> Interpreter(Statement node, ProgramContext program, InterpreterFactory factory)
    {
        return factory.InterpreterNode(node);
    }
}



