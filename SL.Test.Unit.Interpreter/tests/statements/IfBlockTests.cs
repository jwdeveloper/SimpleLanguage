namespace SL.Test.Unit.Interpreter.tests.statements;

public class IfBlockTests : InterpreterTestBase
{
    [Test]
    public async Task ShouldHandleIfTrue()
    {
        var interpreter = await ExecuteProgram("if(true) { print(1); }");

        ProgramAssert.AssertProgram(interpreter)
            .HasConsoleOutput("1", 0);
    }

    [Test]
    public async Task ShouldHandleElse()
    {
        var interpreter = await ExecuteProgram("if(false) {  print(1); } else {  print(2); }");

        ProgramAssert.AssertProgram(interpreter)
            .HasConsoleOutput("2", 0);
    }

    [Test]
    public async Task ShouldHandleElseIf()
    {
        var interpreter = await ExecuteProgram("if(false) {print(1); } else if(false) { print(2); }");

        ProgramAssert.AssertProgram(interpreter)
            .HasConsoleOutputCount(0);
    }
}