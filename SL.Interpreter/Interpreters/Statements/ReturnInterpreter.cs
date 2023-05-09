using SL.Parser.Parsing.AST.Statements;

namespace SL.Interpreter.Interpreters;

public class ReturnInterpreter : IInterpreter<ReturnStatement>
{
    public async Task<object> Interpreter(ReturnStatement node, ProgramContext program, InterpreterFactory factory)
    {
        object? value = null;
        if (node.HasReturnExpression)
        {
            value = await factory.InterpreterNode(node.ReturnExpression);
        }
        var returnType = program.FindObjectType(value);
        return new ProgramReturn(value, returnType);
    }
}