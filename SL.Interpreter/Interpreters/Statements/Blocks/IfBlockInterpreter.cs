using SL.Interpreter.Models;
using SL.Parser.Models.Statements.Blocks;

namespace SL.Interpreter.Interpreters.Statements.Blocks;

public class IfBlockInterpreter : IInterpreter<IfBlockStatement>
{
    public async Task<object> Interpreter(IfBlockStatement node, ProgramContext program, InterpreterFactory factory)
    {
        var conditionResult = await factory.InterpreterNode(node.Condition);
        if (!program.IsBoolean(conditionResult))
        {
            throw new Exception("IF condition should return boolean value");
        }

        var conditionValue = program.GetBoolValue(conditionResult);
        if (conditionValue)
        {
           return await factory.InterpreterNode(node.Body);
        }
        else if (node.HasElseBody)
        {
            return await factory.InterpreterNode(node.ElseBody);
        }

        return null;
    }
}