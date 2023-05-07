using SL.Core.Parsing.AST;
using SL.Core.Parsing.AST.Expressions;

namespace SL.Test.Unit;

public class BlockStatementTests : ParserTestBase
{
    [Test]
    public async Task ShouldParseBlocks()
    {
        //Arrange
        var content = "{ 1.23; { } }";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
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
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
            .HasChildrenCount(3)
            .HasChild<EmptyStatement>(0)
            .HasChild<EmptyStatement>(1)
            .HasChild<EmptyStatement>(2);
    }
}