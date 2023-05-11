using SL.Parser.Parsing.AST.Expressions;

namespace SL.Interpreter.Interpreters;

public class ClassInitializeInterpreter : IInterpreter<ClassInitializeExpressionStatement>
{
 
    public async Task<object> Interpreter(ClassInitializeExpressionStatement node, ProgramContext program, InterpreterFactory factory)
    {
        var className = node.ClassInitializer.FunctionName;
        var parameters = node.ClassInitializer.Paramteters;
        var arguments = new object[parameters.Count];
        for (var i = 0; i < parameters.Count; i++)
        {
            var parameter = parameters[i];
            var result = await factory.InterpreterNode(parameter);
            arguments[i] = result;
        }
       
        var programClass = program.GetClassType(className);
        return programClass.NewInstance(arguments);
    }
}