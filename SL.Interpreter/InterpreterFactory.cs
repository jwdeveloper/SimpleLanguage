using System.Reflection;
using SL.Interpreter.Interpreters;
using SL.Interpreter.Interpreters.Expressions;
using SL.Interpreter.Models;
using SL.Parser.Models;

namespace SL.Interpreter;

public class InterpreterFactory
{
    private readonly ProgramContext _programContext;
    private readonly Dictionary<Type, object> _interpreters;
    private MethodInfo _methodInfo;
    private PropertyInfo _propertyInfo;


    public InterpreterFactory(ProgramContext programContext, Dictionary<Type, object> interpreters)
    {
        _programContext = programContext;
        _interpreters = interpreters;
       
    }


    public async Task<object> InterpreterNode<T>(T node, ProgramContext programContext = null) where T : Node
    {
        var nodeType = node.GetType();
        object interpreter = _interpreters[nodeType];

        if (interpreter == null)
        {
            throw new Exception("Not registered");
        }

        if (_programContext.IsCancelRequested)
        {
            return null;
        }

        if (programContext == null)
        {
            programContext = _programContext;
        }
        
        _methodInfo = interpreter.GetType().GetMethod("Interpreter");
        var task = (Task)_methodInfo.Invoke(interpreter, new object[] { node, programContext, this });
        await task.ConfigureAwait(false);
        _propertyInfo =  task.GetType().GetProperty("Result");
        return _propertyInfo.GetValue(task);
    }
}