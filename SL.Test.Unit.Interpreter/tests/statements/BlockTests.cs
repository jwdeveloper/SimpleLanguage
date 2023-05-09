namespace SL.Test.Unit.Interpreter.tests.statements;

public class BlockTests: InterpreterTestBase
{
 
    
    [Test]
    public async Task ShouldRemoveVariablesAfterBlockExecution()
    {
        var interpreter = await ExecuteProgram("var i =0; { var x = 3; }");
        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("i",0)
            .HasVariableCount(1);
    }
    
    
    [Test]
    public async Task ShouldHandleVariablesWithSameNameInDifferentBlocks()
    {
        var interpreter = await ExecuteProgram("var i =0; { var x = 3; }  {var x = 4} ");
        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("i",0)
            .HasVariableCount(1);
    }
    
}