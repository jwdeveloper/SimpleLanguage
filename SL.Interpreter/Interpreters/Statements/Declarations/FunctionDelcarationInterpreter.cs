using SL.Interpreter.Models;
using SL.Parser.Models.Statements.Declarations;

namespace SL.Interpreter.Interpreters.Statements.Declarations;

public class FunctionDelcarationInterpreter : IInterpreter<FunctionDeclarationStatement>
{
    public async Task<object> Interpreter(FunctionDeclarationStatement node, 
        ProgramContext program,
        InterpreterFactory factory)
    {
        var functionModel = new ProgramFunction();
        functionModel.Name = node.FunctionName;
        functionModel.Type = node.HasFunctionType ? node.FunctionType?.IdentifierName : "var";
        functionModel.Parameters = new string[node.ParameterStatements.Count()];
        functionModel.Invoker = async (input, _program) => await HandleFunctionCall(input, _program, node, factory);
        if (!program.CreateFunction(functionModel))
        {
            throw new Exception($"Unable to create function {node.FunctionName}");  
        }
        
        return functionModel;
    }


    public async Task<object> HandleFunctionCall(object[] input, ProgramContext program, FunctionDeclarationStatement node, InterpreterFactory factory)
    {
        if (input.Length != node.ParameterStatements.Count)
        {
            throw new Exception($"Different number of parameters for function {node.FunctionName}");
        }

        for (var i = 0; i < input.Length; i++)
        {
            var parameter = node.ParameterStatements[i];
            var inputValue = input[i];

            var type = parameter.HasParameterType ? parameter.paramterType.IdentifierName : "var";
            var name = parameter.parameterName.IdentifierName;
            program.CreateVariable(type, name, inputValue);
        }

        var functionResult = await factory.InterpreterNode(node.Body);
        program.RemoveVariable(input.Length);

        return functionResult;
    }
}