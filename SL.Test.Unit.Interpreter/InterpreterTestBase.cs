using SL.Interpreter;
using SL.Test.Unit.Parser;


namespace SL.Test.Unit.Interpreter;

public class InterpreterTestBase : ParserTestBase
{


    public async Task<SL.Interpreter.Evaluator> ExecuteProgram(string code)
    {
        var program = await CreateProgramTree(code);
        var interpreter = CreateInterpreter();
        await interpreter.ExecuteProgram(program);
        return interpreter;
    }

    public SL.Interpreter.Evaluator CreateInterpreter()
    {
       return EvaluatorFactory.CreateEvaluator();
    }
    
}