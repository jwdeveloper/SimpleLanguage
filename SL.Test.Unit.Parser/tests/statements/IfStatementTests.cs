using SL.Parser.Models;
using SL.Parser.Models.Literals;
using SL.Parser.Models.Statements.Blocks;

namespace SL.Test.Unit.Parser.tests.statements;

public class IfStatementTests : ParserTestBase
{
    [Test]
    public async Task ShouldParseIfStatement()
    {
        //Arrange
        var content = "if(true) { }";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<IfBlockStatement>(0, assertion =>
            {
                assertion.Has(e =>
                {
                    NodeAssert.Assert<BoolLiteral>(e.Condition);
                    NodeAssert.Assert<BlockStatement>(e.Body);
                });
            });
    }
    
    [Test]
    public async Task ShouldParseIfElseStatement()
    {
        //Arrange
        var content = "if(true) { 1; 1.23; } else { 2; 5; 3;} ";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<IfBlockStatement>(0, assertion =>
            {
                assertion.Has(e =>
                {
                    NodeAssert.Assert<BoolLiteral>(e.Condition);
                    NodeAssert.Assert<BlockStatement>(e.Body).HasChildrenCount(2);
                    NodeAssert.Assert<BlockStatement>(e.ElseBody).HasChildrenCount(3);
                });
            });
    }
    
    [Test]
    public async Task ShouldParseIfElseIfStatement()
    {
        //Arrange
        var content = "if(true) { 1; 1.23; } else if(false) { 2; 5; 3;} ";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<IfBlockStatement>(0, assertion =>
            {
                assertion.Has(e =>
                {
                    NodeAssert.Assert<BoolLiteral>(e.Condition);
                    NodeAssert.Assert<BlockStatement>(e.Body).HasChildrenCount(2);
                    NodeAssert.Assert<IfBlockStatement>(e.ElseBody);
                });
            });
    }
}