using SL.Interpreter.Models;
using SL.Parser.Models.Statements.Blocks;

namespace SL.Interpreter.Interpreters.Statements.Blocks;

public class BlockInterpreter : IInterpreter<BlockStatement>
{

    public async Task<object> Interpreter(BlockStatement node, ProgramContext program, InterpreterFactory factory)
    {
        var beginVariableCount = program.Variables.Count;
        foreach(var statement in node.Statements)
        {
          var result =  await factory.InterpreterNode(statement);
          if (result is ProgramReturn)
          {
              RemoveDelcaredVariables(beginVariableCount, program);
              return result;
          }
          if (result is BreakOperation)
          {
              return result;
          }
        }
        RemoveDelcaredVariables(beginVariableCount, program);
        return null;
    }

    public void RemoveDelcaredVariables(int beginCount, ProgramContext programContext)
    {
        var endVariableCount = programContext.Variables.Count;
        programContext.RemoveVariable(endVariableCount-beginCount);
    }
}