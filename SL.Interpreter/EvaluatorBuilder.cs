using SL.Interpreter.Interpreters;
using SL.Interpreter.Models;
using SL.Parser.Models;

namespace SL.Interpreter;

public class EvaluatorBuilder
{
    private Dictionary<Type, object> Interpreters;
    private ProgramContext programContext;
    
    public EvaluatorBuilder(CancellationToken ctx)
    {
        Interpreters = new Dictionary<Type, object>();
        programContext = new ProgramContext(ctx);
    }


    public EvaluatorBuilder WithConsoleEvent(Action<string> consoleEvent)
    {
        return this;
    }

    public EvaluatorBuilder WithSystemFunction(string name, string type,
        Func<object[], ProgramContext, Task<object>> invoker = null)
    {
        var functionModel = new ProgramFunction();
        functionModel.Type = type;
        functionModel.Name = name;
        functionModel.Invoker = invoker;
        programContext.CreateFunction(functionModel);
        return this;
    }


    public EvaluatorBuilder WithInterpreter<Nodee, Inter>() where Nodee : Node where Inter : new()
    {
        var inter = new Inter();
        var node = typeof(Nodee);
        Interpreters.Add(node, inter);
        return this;
    }

    public EvaluatorBuilder WithInterpreter<Nodee>(DefaultInterpreter<Nodee>.DefaultAction<Nodee> action)
        where Nodee : Node
    {
        var inter = new DefaultInterpreter<Nodee>(action);
        var node = typeof(Nodee);
        Interpreters.Add(node, inter);
        return this;
    }


    public Evaluator Build()
    {
        var factory = new InterpreterFactory(programContext, Interpreters);
        return new Evaluator(factory, programContext);
    }
}