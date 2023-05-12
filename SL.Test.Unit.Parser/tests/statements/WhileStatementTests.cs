using SL.Parser.Models;
using SL.Parser.Models.Literals;
using SL.Parser.Models.Statements;
using SL.Parser.Models.Statements.Blocks;

namespace SL.Test.Unit.Parser.tests.statements;

public class WhileStatementTests  : ParserTestBase
{
    [Test]
    public async Task ShouldParseWhileStatement()
    {
        //Arrange
        var content = "while(true) { 1; 1.23; } ";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<WhileBlockStatement>(0, assertion =>
            {
                assertion.Has(e =>
                {
                    Assert.That(e.IsDoWhile, Is.EqualTo(false));
                    NodeAssert.Assert<BoolLiteral>(e.Condition);
                    NodeAssert.Assert<BlockStatement>(e.Body).HasChildrenCount(2);
                });
            });
    }
    
    
    [Test]
    public async Task ShouldParseDoWhileStatement()
    {
        //Arrange
        var content = "do { 1; 1.23; } while(true) ";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<WhileBlockStatement>(0, assertion =>
            {
                assertion.Has(e =>
                {
                    Assert.That(e.IsDoWhile, Is.EqualTo(true));
                    NodeAssert.Assert<BoolLiteral>(e.Condition);
                    NodeAssert.Assert<BlockStatement>(e.Body).HasChildrenCount(2);
                });
            });
    }
}