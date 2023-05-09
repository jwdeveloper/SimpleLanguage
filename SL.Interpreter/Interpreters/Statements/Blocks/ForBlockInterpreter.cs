using SL.Core.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public class ForBlockInterpreter : IInterpreter<ForStatement>
{
    public async Task<object> Interpreter(ForStatement node, ProgramContext program, InterpreterFactory factory)
    {

        if (node.HasDeclaration)
        {
           await factory.InterpreterNode(node.Declaration);
        }
        
        var stackOverflowIterations = 1000;
        while (await GetConditionResult(node, program, factory) &&  !program.IsCancelRequested)
        {
            await factory.InterpreterNode(node.Body);
            if (node.HasAssigment)
            {
                await factory.InterpreterNode(node.Assigment);
            }
      
            stackOverflowIterations--;
            if (stackOverflowIterations  < 0)
            {
                throw new Exception("InfiniteLoop");
            }
        }

        return true;
       
    }
    
    
    private async Task<bool> GetConditionResult(ForStatement forStatement, ProgramContext program, InterpreterFactory factory)
    {
        if (!forStatement.HasCondition)
        {
            return true;
        }
        
        
        var conditionResult = await factory.InterpreterNode(forStatement.Condition);
        if (!program.IsBoolean(conditionResult))
        {
            throw new Exception("IF condition should return boolean value");
        }
        return program.GetBoolValue(conditionResult);
    }
}