using SL.Core.Parsing.AST.Expressions;

namespace SL.Interpreter.Interpreters.Expressions;

public class IdentifierExpressionInterpreter  : IInterpreter<IdentifierLiteral> 
{
    public async Task<object> Interpreter(IdentifierLiteral node, ProgramContext program, InterpreterFactory factory)
    {
        var identiferName = node.IdentifierName;

        if (!program.IsVariableExists(identiferName))
        {
            throw new Exception($"Variable not exists for identifier {identiferName}");
        }
        return program.GetVariable(identiferName);
    }
}