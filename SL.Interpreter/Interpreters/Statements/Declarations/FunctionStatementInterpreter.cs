using SL.Parser.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public class FunctionStatementInterpreter : IInterpreter<FunctionDeclarationStatement>
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
        return program.CreateFunction(functionModel);
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