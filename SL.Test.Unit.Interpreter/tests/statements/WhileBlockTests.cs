namespace SL.Test.Unit.Interpreter.tests.statements;

public class WhileBlockTests : InterpreterTestBase
{
    [Test] public async Task ShouldHandleWhile()
    {
        var interpreter = await ExecuteProgram("var i =0; while(i<3) { i +=1; }");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("i", 3);
    }
    
    
    [Test] public async Task ShouldHandleDoWhile()
    {
        var interpreter = await ExecuteProgram("var i =0; do { i +=1; } while(i < 3) ");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("i", 3);
    }
    
    [Test] 
    public async Task ShouldHandleInfiteLoop()
    {
        ProgramAssert.AssertThrowsProgram<Exception>(async () =>
        {
            await ExecuteProgram(" while(true) {  }");
        });
    }
}