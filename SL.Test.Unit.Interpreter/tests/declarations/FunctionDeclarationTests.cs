namespace SL.Test.Unit.Interpreter.tests.declarations;

public class FunctionDeclarationTests : InterpreterTestBase
{
    [Test]
    public async Task ShouldDeclareFunction()
    {
        var interpreter = await ExecuteProgram( "function hello(age, name) { }");
        ProgramAssert.AssertProgram(interpreter)
            .HasFunction("hello", 2);
    }
    
    
    [Test]
    public async Task ShouldDeclareFunctionWithType()
    {
        var interpreter = await ExecuteProgram( "function number hello(age, name) { }");
        ProgramAssert.AssertProgram(interpreter)
            .HasFunction("hello", "number", 2);
    }
}