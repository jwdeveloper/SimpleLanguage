namespace SL.Test.Unit.Interpreter.tests;

public class FunctionCallTests : InterpreterTestBase
{
    [Test]
    public async Task ShouldDeclareFunction()
    {
        var interpreter = await ExecuteProgram(@"
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
        var interpreter = await ExecuteProgram(@"
         print(""hello world"");
         clear();");
        ProgramAssert.AssertProgram(interpreter)
            .HasConsoleOutputCount(0);
    }


    [Test]
    public async Task ShouldReturnValue()
    {
        var interpreter = await ExecuteProgram(@"
         var i = 0;
         function getAge()
         {
           if(i == 0)
           {
             return 100;
           } 
           return 10;
         } 
         i = getAge();");


        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(1)
            .HasVariable("i", 100);
    }


    [Test]
    public async Task ShouldThrowsWhenFunctionNotReturn()
    {
        ProgramAssert.AssertThrowsProgram<Exception>(async () =>
        {
            var interpreter = await ExecuteProgram(@"
         var i = 0;
         function number getAge()
         {
            
         } 
         i = getAge();");
        });
    }
    
    
    [Test]
    public async Task ShouldReturnFromFirstStatement()
    {
        var interpreter = await ExecuteProgram(@"
         var i = 0;
         function number getAge()
         {
            return 10;
            return 20;
         } 
         i = getAge();");
        ProgramAssert.AssertProgram(interpreter)
            .HasFunction("getAge")
            .HasVariable("i", 10);
    }
    
    
    [Test]
    public async Task ShouldExitFunction()
    {
        var interpreter = await ExecuteProgram(@"
         function display()
         {
            print(""before"");
            return;
            print(""after"");
         } 
          display();");
        ProgramAssert.AssertProgram(interpreter)
            .HasConsoleOutputCount(1)
            .HasConsoleOutput("before", 0);
    }
}