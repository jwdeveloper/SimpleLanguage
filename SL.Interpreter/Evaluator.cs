using SL.Core.Parsing.AST;

namespace SL.Interpreter;

public class Evaluator
{
    private readonly InterpreterFactory _factory;
    private readonly ProgramContext _programContext;

    public Evaluator(InterpreterFactory factory, ProgramContext programContext)
    {
        _factory = factory;
        _programContext = programContext;
    }


    public ProgramContext ProgramContext => _programContext;
    
    public async Task ExecuteProgram(SlProgram program, CancellationToken ctx = new CancellationToken())
    {
        await _factory.InterpreterNode(program);
    }

    public void OnConsoleUpdate(Action<List<string>> onConsole)
    {
        _programContext.onConsoleUpdate = onConsole;
    }

  
}