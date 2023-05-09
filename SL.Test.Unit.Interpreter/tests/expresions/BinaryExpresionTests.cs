namespace SL.Test.Unit.Interpreter.tests;

public class BinaryExpresionTests : InterpreterTestBase
{
    
    [Test]
    public async Task ShouldParseComplexExpression()
    {
        var interpreter = await ExecuteProgram("var y = 12; var x = (2 > 1 && 3 > 1) && y == 12;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", true);
    }
    
    [Test]
    public async Task ShouldHandleGrater()
    {
        var interpreter = await ExecuteProgram("var x = 2 > 1;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", true);
    }
    
    [Test]
    public async Task ShouldHandleLesser()
    {
        var interpreter = await ExecuteProgram("var x = 2 < 1;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", false);
    }
    
    [Test]
    public async Task ShouldHandleEqual()
    {
        var interpreter = await ExecuteProgram("var x = 1 == 1;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", true);
    }
    
    [Test]
    public async Task ShouldHandleEqualWithIs()
    {
        var interpreter = await ExecuteProgram("var x = 1 is 1;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", true);
    }
    
    [Test]
    public async Task ShouldHandleNotEqual()
    {
        var interpreter = await ExecuteProgram("var x = 1 != 2;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", true);
    }
    
    [Test]
    public async Task ShouldHandleNotEqualWithFalse()
    {
        var interpreter = await ExecuteProgram("var x = 1 != 1;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", false);
    }
    
    [Test]
    public async Task ShouldHandleNotEqualForStrings()
    {
        var interpreter = await ExecuteProgram("var x = \"Hello\" == \"Hello\";");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", true);
    }
    
    
    [Test]
    public async Task ShouldHandleGraterEqual()
    {
        var interpreter = await ExecuteProgram("var x = 1 >= 1;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", true);
    }
    
    [Test]
    public async Task ShouldHandleLesserEqual()
    {
        var interpreter = await ExecuteProgram("var x = 1 <= 1;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", true);
    }
    
    [Test]
    public async Task ShouldHandleAddFromStrings()
    {
        var interpreter = await ExecuteProgram("var x = \"hello \" + \"world\";");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", "hello world");
    }
    
    
    [Test]
    public async Task ShouldHandleAdd()
    {
        var interpreter = await ExecuteProgram("var x = 3 + 3;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", 6);
    }
    
    [Test]
    public async Task ShouldHandleMinus()
    {
        var interpreter = await ExecuteProgram("var x = 3 - 3;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", 0);
    }
    
    [Test]
    public async Task ShouldHandleMutliple()
    {
        var interpreter = await ExecuteProgram("var x = 3 * 3;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", 9);
    }
    
    [Test]
    public async Task ShouldHandleDivde()
    {
        var interpreter = await ExecuteProgram("var x = 3 / 3;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", 1);
    }
    
    [Test]
    public async Task ShouldHandlePower()
    {
        var interpreter = await ExecuteProgram("var x = 3 ^ 3;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", 27);
    }
    
    
    [Test]
    public async Task ShouldHandleAndTrue()
    {
        var interpreter = await ExecuteProgram("var x = true and true;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", true);
    }
    
    [Test]
    public async Task ShouldHandleAndFalse()
    {
        var interpreter = await ExecuteProgram("var x = true and false;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", false);
    }
    
    [Test]
    public async Task ShouldHandleOrTrue()
    {
        var interpreter = await ExecuteProgram("var x = true or true;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", true);
    }
    
    [Test]
    public async Task ShouldHandleOrFalse()
    {
        var interpreter = await ExecuteProgram("var x = false  or false;");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("x", false);
    }
    
}