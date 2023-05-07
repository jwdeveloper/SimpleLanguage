using SL.Core.Parsing.AST;
using SL.Core.Parsing.AST.Expressions;

namespace SL.Test.Unit;

public class ForStatementTests : ParserTestBase
{
    [Test]
    public async Task ShouldParseForStatement()
    {
        //Arrange
        var content = "for(var i =0; i < 10; i+=1) {  x +=1; } ";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
            .HasChild<ForStatement>(0, assertion =>
            {
               
            });
    }
    
    [Test]
    public async Task ShouldWorkInfiteLoop()
    {
        //Arrange
        var content = "for(;;) {  x +=1; } ";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
            .HasChild<ForStatement>(0, assertion =>
            {
               
            });
    }
    
    [Test]
    public async Task ShouldParseForeachStatement()
    {
        //Arrange
        var content = "for(var i in objects) {  } ";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
            .HasChild<ForeachStatement>(0, assertion =>
            {
             
            });
    }
}