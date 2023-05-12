using SL.Interpreter.Models;
using SL.Parser.Models.Expressions;

namespace SL.Interpreter.Interpreters.Expressions;

public class FunctionCallExpressionInterpreter : IInterpreter<FunctionCallExpression>
{
    public async Task<object> Interpreter(FunctionCallExpression node, ProgramContext program,
        InterpreterFactory factory)
    {
        var parameters = node.Paramteters;
        var arguments = new object[parameters.Count];
        var i = 0;
        foreach (var parameter in parameters)
        {
            var result = await factory.InterpreterNode(parameter);
            arguments[i] = result;
            i++;
        }

        var functionModel = program.GetFunction(node.FunctionName);
        var functionResult = await program.InvokeFunction(node.FunctionName, arguments);


        if (functionResult is null)
        {
            if (functionModel.Type == "var")
            {
                return null;
            }
            else
            {
                throw new Exception($"Function has declared type  {functionModel.Type} but does not return anything");
            }
        }
        else if (functionResult is not ProgramReturn)
        {
            functionResult = new ProgramReturn(functionResult);
        }


        if (functionResult is ProgramReturn programReturn)
        {
            if (!program.IsValueMatchType(functionModel.Type, programReturn.Value))
            {
                throw new Exception(
                    $"Return value  {programReturn.Value} not match function type  {functionModel.Type}");
            }
            return programReturn.Value;
        }

        throw new Exception($"Unexpected function return value {functionResult}");
    }
}