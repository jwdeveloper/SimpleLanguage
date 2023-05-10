

using SL.Parser.Api.Exceptions;

namespace SL.Test.Unit.Interpreter.tests;

public class VariableAssigmentTests: InterpreterTestBase
{
    [Test]
    public async Task ShouldAssigneNumberToVar()
    {
        var interpreter = await ExecuteProgram("var x; x = 12;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(1)
            .HasVariable("x", 12);
    }
    
    
    [Test]
    public async Task ShouldAssignReference()
    {
        var interpreter = await ExecuteProgram("var x; var y = 12; x = y;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(2)
            .HasVariable("x", 12)
            .HasVariable("y", 12);
    }
    
    [Test]
    public async Task ShouldHandleChainAssignment()
    {
        var interpreter = await ExecuteProgram("var x; var y = 12; x = y = 1;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(2)
            .HasVariable("x", 1)
            .HasVariable("y", 1);
    }
    
    
    [Test]
    public async Task ShouldHandleAdd()
    {
        var interpreter = await ExecuteProgram("var x = 2; x += 2;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(1)
            .HasVariable("x", 4);
    }
    
    [Test]
    public async Task ShouldHandleAddString()
    {
        var interpreter = await ExecuteProgram("var x = \"hello \"; x += \"world\";");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(1)
            .HasVariable("x", "hello world");
    }
    
    [Test]
    public async Task ShouldHandleMinus()
    {
        var interpreter = await ExecuteProgram("var x = 2; x -= 2;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(1)
            .HasVariable("x", 0);
    }
    
    [Test]
    public async Task ShouldHandleMultiply()
    {
        var interpreter = await ExecuteProgram("var x = 2; x *= 2;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(1)
            .HasVariable("x", 4);
    }
    
    [Test]
    public async Task ShouldHandleDivde()
    {
        var interpreter = await ExecuteProgram("var x = 2; x /= 2;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(1)
            .HasVariable("x", 1);
    }
    
    [Test]
    public async Task ShouldHandlePower()
    {
        var interpreter = await ExecuteProgram("var x = 2; x ^= 2;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariableCount(1)
            .HasVariable("x", 4);
    }
    
    
    [Test]
    public async Task ShouldThrowWhenThereAreNotVariable()
    {
        ProgramAssert.AssertThrowsProgram<SyntaxException>(
                async () =>
                {
                    await ExecuteProgram("2 = 2;");
                });
    }
    
    [Test]
    public async Task ShouldThrowWhenMathematicalOperationIfPerformeOnNotNumericType()
    {
        ProgramAssert.AssertThrowsProgram<Exception>(
            async () =>
            {
                await ExecuteProgram("var x = false; x +=1;");
            });
    }
}