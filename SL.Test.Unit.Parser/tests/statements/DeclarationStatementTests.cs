using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Test.Unit.Parser;

public class DeclarationStatement : ParserTestBase
{
    [Test]
    public async Task ShouldHandleDeclarationAssigment()
    {
        //Arrange
        var content = "var name = 12;";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program);
    }
    
    [Test]
    public async Task ShouldHandleSingleVariable()
    {
        //Arrange
        var content = "var name;";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program);
    }
    
    [Test]
    public async Task ShouldHandleMultipleVariables()
    {
        
        //Arrange
        var content = "var name, age, gender =1;";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program);
    }
}