using SL.Core.Parsing.AST.Expressions;

namespace SL.Interpreter.Interpreters.Expressions;

public class FunctionCallExpressionInterpreter : IInterpreter<FunctionCallExpression>
{
    public async Task<object> Interpreter(FunctionCallExpression node, ProgramContext program, InterpreterFactory factory)
    {
        var parameters = node.Paramteters;
        var arguments = new object[parameters.Count];
        var i = 0;
        foreach(var parameter  in parameters)
        {
            var result = await factory.InterpreterNode(parameter);
            arguments[i] = result;
            i++;
        }
        return program.InvokeFunction(node.FunctionName, arguments);
    }
}