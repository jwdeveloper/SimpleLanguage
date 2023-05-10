namespace SL.Test.Unit.Interpreter.tests;

public class ClassDeclarationTests: InterpreterTestBase
{
    [Test]
    public async Task ShouldDeclareSimpleClass()
    {
        var interpreter = await ExecuteProgram( "class User {}");
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