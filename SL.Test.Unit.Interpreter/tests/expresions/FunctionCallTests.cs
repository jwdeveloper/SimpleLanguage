namespace SL.Test.Unit.Interpreter.tests;

public class FunctionCallTests : InterpreterTestBase
{
    [Test]
    public async Task ShouldDeclareFunction()
    {
        var interpreter = await ExecuteProgram( @"
         var i = 0;
         function hello(age)
         {
            i = age;  
            print(""my age is"", age);
         } 
         hello(100);");
        ProgramAssert.AssertProgram(interpreter)
            .HasFunction("hello", 1)
            .HasVariableCount(1)
            .HasVariable("i", 100)
            .HasConsoleOutput($"my age is {100}", 0);
    }
    
    [Test]
    public async Task ShouldClearConsole()
    {
        var interpreter = await ExecuteProgram( @"
         print(""hello world"");
         clear();");
        ProgramAssert.AssertProgram(interpreter)
            .HasConsoleOutputCount(0);
    }

}