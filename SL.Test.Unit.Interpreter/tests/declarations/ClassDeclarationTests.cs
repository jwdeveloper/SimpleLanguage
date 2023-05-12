namespace SL.Test.Unit.Interpreter.tests.declarations;

public class ClassDeclarationTests : InterpreterTestBase
{
    [Test]
    public async Task ShouldDeclareSimpleClass()
    {
        var interpreter = await ExecuteProgram("class User {}");
        ProgramAssert.AssertProgram(interpreter)
            .HasClass("User");
    }


    [Test]
    public async Task ShouldDeclareFunctionWithType()
    {
        var interpreter = await ExecuteProgram("class User(name, age) {  function getName() { return name; }   }");
        ProgramAssert.AssertProgram(interpreter)
            .HasClass("User", @class =>
            {
                Assert.That(@class.Fields.Count, Is.EqualTo(2));
                Assert.That(@class.Functions.Count, Is.EqualTo(1));
                Assert.That(@class.Consturctors.Count, Is.EqualTo(1));
            });
    }
}