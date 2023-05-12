namespace SL.Test.Unit.Interpreter.tests.declarations;

public class ShouldInterpreter : InterpreterTestBase
{
    [Test]
    public async Task ShouldDeclareVariableWithValue()
    {
        var interpreter = await ExecuteProgram( "var i, x =0;");
        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(2)
            .HasVariable("i", 0)
            .HasVariable("x", 0);
    }


    [Test]
    public async Task ShouldDeclareVariableWithoutValue()
    {
        var interpreter = await ExecuteProgram( "var i, x;");
        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(2)
            .HasVariable("i", null)
            .HasVariable("x", null);
    }


    [Test]
    public async Task ShouldDeclareReference()
    {
        var interpreter = await ExecuteProgram( "var x = 2; var i = x;");
        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(2)
            .HasVariable("x", 2)
            .HasVariable("i", 2);
    }

    [Test]
    public async Task ShouldAssigneNumberToVar()
    {
        var interpreter = await ExecuteProgram("number x = 11; var i = x;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(2)
            .HasVariable("x", 11)
            .HasVariable("i", 11);
    }

    [Test]
    public void ShouldThrowsWhenTypesNotMatchs()
    {
        ProgramAssert.AssertThrowsProgram<Exception>(async () =>
        {
            await ExecuteProgram( "number x = false;");
        });
    }
    
    [Test]
    public void ShouldThrowsWhenVariableIsNotFound()
    {
        ProgramAssert.AssertThrowsProgram<Exception>(async () =>
        {
            await ExecuteProgram( "var x = y;");
        });
    }
}