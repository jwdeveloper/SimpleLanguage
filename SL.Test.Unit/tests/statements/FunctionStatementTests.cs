using SL.Core.Parsing.AST;

namespace SL.Test.Unit;

public class FunctionStatementTests : ParserTestBase
{
    
    
    
    [Test]
    public async Task ShouldParseFunction()
    {
        //Arrange
        var content = "function number GetName(text name, number age) { var i =0; return; }";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
            .HasChild<FunctionDeclarationStatement>(0);
    }
    
    [Test]
    public async Task ShouldParseFunctionWithoutType()
    {
        //Arrange
        var content = "function GetName() { var i =0;  return 2 + 3; }";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
            .HasChild<FunctionDeclarationStatement>(0);
    }

}