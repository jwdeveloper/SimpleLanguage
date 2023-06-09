using SL.Parser.Models;
using SL.Parser.Models.Statements;
using SL.Parser.Models.Statements.Blocks;

namespace SL.Test.Unit.Parser.tests.statements;

public class BlockStatementTests : ParserTestBase
{
    [Test]
    public async Task ShouldParseBlocks()
    {
        //Arrange
        var content = "{ 1.23; { } }";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChildrenCount(1)
            .HasChild<BlockStatement>(0,
                e =>
                {
                    e.HasChildrenCount(2);
                    e.HasChild<ExpresionStatement>(0);
                    e.HasChild<BlockStatement>(1);
                });
    }
    
    [Test]
    public async Task ShouldParseEmptyStatement()
    {
        //Arrange
        var content = "; ; ;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChildrenCount(3)
            .HasChild<EmptyBlockStatement>(0)
            .HasChild<EmptyBlockStatement>(1)
            .HasChild<EmptyBlockStatement>(2);
    }
}