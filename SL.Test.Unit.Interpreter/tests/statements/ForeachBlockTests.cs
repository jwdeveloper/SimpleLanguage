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
    
    
    [Test]
    public async Task ShouldHandleNestedLoop()
    {
        var interpreter= await ExecuteProgram(@"
            for(var i in range(3)) 
            {
              for(var y in range(i))
              {
                print(y);
              }
            }");
        ProgramAssert.AssertProgram(interpreter)
            .HasConsoleOutputCount(3)
            .HasConsoleOutput("0", 0)
            .HasConsoleOutput("0", 1)
            .HasConsoleOutput("1", 2);

    }
}