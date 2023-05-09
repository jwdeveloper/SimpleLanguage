using SL.Core.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public class BlockInterpreter : IInterpreter<BlockStatement>
{

    public async Task<object> Interpreter(BlockStatement node, ProgramContext program, InterpreterFactory factory)
    {
        foreach(var statement in node.Statements)
        {
            await factory.InterpreterNode(statement);
        }
        return true;
    }
}