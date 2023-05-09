using SL.Parser.Parsing.AST.Expressions;

namespace SL.Interpreter.Interpreters;

public class ExpresionStatementInterpreter : IInterpreter<ExpresionStatement>
{
    public async Task<object> Interpreter(ExpresionStatement node, ProgramContext program, InterpreterFactory factory)
    {
        return await factory.InterpreterNode(node.Expression);
    }
}