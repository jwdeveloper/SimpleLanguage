namespace SL.Test.Unit.Interpreter.tests.statements;

public class ForBlockTests: InterpreterTestBase
{
    [Test]
    public async Task ShouldHandleFor()
    {
        var interpreter = await ExecuteProgram("var i =0; for(var x =0; x<3;x+=1) {  i = x; }");

        ProgramAssert.AssertProgram(interpreter)
            .HasVariable("i", 2);
    }
    
    
    [Test]
    public async Task ShouldHandleInfitieFor()
    {
        ProgramAssert.AssertThrowsProgram<Exception>(async () =>
        {
            await ExecuteProgram("var i =0; for(;;) {  i = 3; }");
        });
    }
    
    
      
   
    
}