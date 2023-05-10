using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Statements;

namespace SL.Interpreter.Interpreters;

public class WhileBlockInterpeter : IInterpreter<WhileBlockStatement>
{
    public async Task<object> Interpreter(WhileBlockStatement node, ProgramContext program, InterpreterFactory factory)
    {
        if (node.IsDoWhile)
        {
            await factory.InterpreterNode(node.Body);
        }

        var stackOverflowIterations = 1000;
        var conditionResult = await GetConditionResult(node.Condition, program, factory);
        while (conditionResult &&  !program.IsCancelRequested)
        {
            var result = await factory.InterpreterNode(node.Body);
            if (result is ProgramReturn returnStatement)
            {
                return returnStatement;
            }
            if (result is BreakOperation)
            {
                return null;
            }
            
            conditionResult = await GetConditionResult(node.Condition, program, factory);
            stackOverflowIterations--;

            if (stackOverflowIterations  < 0)
            {
                throw new Exception("InfiniteLoop");
            }
        }

        return true;
    }


    private async Task<bool> GetConditionResult(Expression condition, ProgramContext program, InterpreterFactory factory)
    {
        var conditionResult = await factory.InterpreterNode(condition);
        if (!program.IsBoolean(conditionResult))
        {
            throw new Exception("IF condition should return boolean value");
        }
        return program.GetBoolValue(conditionResult);
    }

    
}