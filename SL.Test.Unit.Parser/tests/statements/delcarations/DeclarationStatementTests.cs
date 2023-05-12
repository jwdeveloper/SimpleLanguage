using SL.Parser.Models;

namespace SL.Test.Unit.Parser.tests.statements.delcarations;

public class DeclarationStatement : ParserTestBase
{
    [Test]
    public async Task ShouldHandleDeclarationAssigment()
    {
        //Arrange
        var content = "var name = 12;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program);
    }
    
    [Test]
    public async Task ShouldHandleSingleVariable()
    {
        //Arrange
        var content = "var name;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program);
    }
    
    [Test]
    public async Task ShouldHandleMultipleVariables()
    {
        
        //Arrange
        var content = "var name, age, gender =1;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program);
    }
}