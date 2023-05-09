using SL.Core.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public class FunctionStatementInterpreter : IInterpreter<FunctionDeclarationStatement>
{
    public async Task<object> Interpreter(FunctionDeclarationStatement node, ProgramContext program,
        InterpreterFactory factory)
    {
        var functionModel = new ProgramFunction();
        functionModel.Name = node.FunctionName;
        functionModel.Type = node.HasFunctionType ? node.FunctionType?.IdentifierName : "var";
        functionModel.Parameters = new string[node.ParameterStatements.Count()];
        functionModel.Invoker = async (input, program) =>
        {
            if (input.Length != node.ParameterStatements.Count)
            {
                throw new Exception($"Different number of parameters for function {node.FunctionName}");
            }

            var varableNames = new string[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                var parameter = node.ParameterStatements[i];
                var inputValue = input[i];
                
                var type = parameter.HasParameterType ? parameter.paramterType.IdentifierName : "var";
                var name = parameter.parameterName.IdentifierName;
                program.CreateVariable(type, name, inputValue);

                varableNames[i] = name;
            }
            var functionResult = await factory.InterpreterNode(node.Body);
            
            foreach(var variable in varableNames)
            {
                program.RemoveVariable(variable);
            }
            
            return functionResult;
        };
        return program.CreateFunction(functionModel);
    }
}