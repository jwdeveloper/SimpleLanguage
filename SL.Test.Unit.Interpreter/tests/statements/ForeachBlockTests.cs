namespace SL.Test.Unit.Interpreter.tests.statements;

public class ForeachBlockTests : InterpreterTestBase
{
    [Test]
    public async Task ShouldHandleInfitieFor()
    {
       var interpreter= await ExecuteProgram("for(var i in range(2)) { print(i); }");

       ProgramAssert.AssertProgram(interpreter)
           .HasConsoleOutputCount(2)
           .HasConsoleOutput("0", 0)
           .HasConsoleOutput("1", 1);

    }
}