using SL.Core.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public class IfBlockInterpreter : IInterpreter<IfStatement>
{
    public async Task<object> Interpreter(IfStatement node, ProgramContext program, InterpreterFactory factory)
    {
        var conditionResult = await factory.InterpreterNode(node.Condition);
        if (!program.IsBoolean(conditionResult))
        {
            throw new Exception("IF condition should return boolean value");
        }

        var conditionValue = program.GetBoolValue(conditionResult);
        if (conditionValue)
        {
            await factory.InterpreterNode(node.Body);
        }
        else if (node.HasElseBody)
        {
            await factory.InterpreterNode(node.ElseBody);
        }

        return true;
    }
}