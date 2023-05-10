
using SL.Interpreter;
using SL.Parser.Parsing.AST;

namespace SL.Test.Unit.Interpreter;

public class ProgramAssert
{
    
    public static void AssertThrowsProgram<T>(Func<Task> action) where T: Exception
    {
        Assert.ThrowsAsync<T>(async () =>
        {
            await action.Invoke();
        });
    }
    
    public static void AssertThrowsProgram<T>(Evaluator evaluator, SlProgram slProgram) where T: Exception
    {
        Assert.ThrowsAsync<T>(async () =>
        {
            await evaluator.ExecuteProgram(slProgram);
        });
    }
    public static ProgramAssert AssertProgram(Evaluator evaluator)
    {
        return new ProgramAssert(evaluator);
    }


    private readonly Evaluator _evaluator;

    public ProgramAssert(Evaluator evaluator)
    {
        _evaluator = evaluator;
    }


    public ProgramAssert HasVariableCount(int count)
    {
        Assert.That(_evaluator.ProgramContext.Variables.Count, Is.EqualTo(count));
        return this;
    }

    public ProgramAssert HasVariable(string name)
    {
        Assert.That(_evaluator.ProgramContext.IsVariableExists(name), Is.EqualTo(true));
        return this;
    }

    public ProgramAssert HasVariable(string name, object value)
    {
        HasVariable(name);
        var variableValue = _evaluator.ProgramContext.Variables[name].Value;
        Assert.That(variableValue, Is.EqualTo(value));
        return this;
    }
    
    
    public ProgramAssert HasFunctionsCount(int count)
    {
        Assert.That(_evaluator.ProgramContext.Functions.Count, Is.EqualTo(count));
        return this;
    }

    public ProgramAssert HasFunction(string name)
    {
        Assert.That(_evaluator.ProgramContext.IsFunctionExists(name), Is.EqualTo(true));
        return this;
    }

    public ProgramAssert HasFunction(string name, int parameters)
    {
        HasFunction(name);
        var variableValue = _evaluator.ProgramContext.Functions[name];
        Assert.That(variableValue.Parameters.Count, Is.EqualTo(parameters));
        return this;
    }
    
    public ProgramAssert HasFunction(string name, string type, int parameters)
    {
        HasFunction(name);
        var variableValue = _evaluator.ProgramContext.Functions[name];
        Assert.That(variableValue.Parameters.Count, Is.EqualTo(parameters));
        Assert.That(variableValue.Type, Is.EqualTo(type));
        return this;
    }
    
    public ProgramAssert HasConsoleOutput(string output, int index)
    {
        var value = _evaluator.ProgramContext.Console[index];
        Assert.That(value, Is.EqualTo(output));
        return this;
    }
    
    public ProgramAssert HasConsoleOutputCount(int count)
    {
        Assert.That(_evaluator.ProgramContext.Console.Count, Is.EqualTo(count));
        return this;
    }
}