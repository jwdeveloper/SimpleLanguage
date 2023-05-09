using SL.Parser.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public class ForeachBlockInterpreter : IInterpreter<ForeachStatement>
{
    public async Task<object> Interpreter(ForeachStatement node, ProgramContext program, InterpreterFactory factory)
    {
        var declaration =  await factory.InterpreterNode(node.Declaration);
        var variables = declaration as List<ProgramVariable>;
        if (variables is null || variables.Count != 1)
        {
            throw new Exception("Bad declaration of variable");
        }

        var programVariable = variables[0];

        var iterator = await factory.InterpreterNode(node.Iterator);
        var forIterator = iterator as ProgramVariable;
        if (forIterator is null || forIterator.type != "list")
        {
            throw new Exception("Iterator must has List type");
        }


        var list = forIterator.value as List<object>;
        var stackOverflowIterations = 1000;
        for (var i = 0; i < list.Count; i++)
        {
            programVariable.value = list[i];
            await factory.InterpreterNode(node.Body);
            stackOverflowIterations--;
            if (stackOverflowIterations  < 0)
            {
                throw new Exception("InfiniteLoop");
            }
        }

       
        return true;
    }
    
}