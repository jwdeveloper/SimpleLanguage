using System.Reflection;
using SL.Core.Parsing.AST;
using SL.Core.Parsing.AST.Expressions;
using SL.Interpreter.Interpreters;
using SL.Interpreter.Interpreters.Expressions;

namespace SL.Interpreter;

public class InterpreterFactory
{
    private readonly ProgramContext ProgramContext;
    private Dictionary<Type, object> Interpreters;


    public InterpreterFactory(ProgramContext programContext, Dictionary<Type, object> interpreters)
    {
        this.ProgramContext = programContext;
        Interpreters = interpreters;
    }

  
    public async Task<object> InterpreterNode<T>(T node) where T : Node
    {
        var nodeType = node.GetType();
        object interpreter = Interpreters[nodeType];

        if (interpreter == null)
        {
            throw new Exception("Not registered");
        }
        var method = interpreter.GetType().GetMethod("Interpreter");
        
        var task = (Task)method.Invoke(interpreter, new object []{node, ProgramContext, this});
        await task.ConfigureAwait(false);
        var resultProperty = task.GetType().GetProperty("Result");
        return resultProperty.GetValue(task);
    }
    
}