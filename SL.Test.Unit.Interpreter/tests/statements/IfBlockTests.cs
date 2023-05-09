namespace SL.Test.Unit.Interpreter.tests.statements;

public class IfBlockTests : InterpreterTestBase
{
    [Test]
    public async Task ShouldHandleIfTrue()
    {
        var interpreter = await ExecuteProgram("if(true) { var x =0; }");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", 0);
    }
    
    [Test]
    public async Task ShouldHandleElse()
    {
        var interpreter = await ExecuteProgram("if(false) { var x =0; } else { var y = 2; }");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(1)
            .HasVariable("y", 2);
    }
    
    [Test]
    public async Task ShouldHandleElseIf()
    {
        var interpreter = await ExecuteProgram("if(false) { var x =0; } else if(false) { var y = 2; }");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(0);
    }
}