using SL.Parser.Models;
using SL.Parser.Models.Statements.Blocks;

namespace SL.Test.Unit.Parser.tests.statements;

public class ForeachStatementTests : ParserTestBase
{
    [Test]
    public async Task ShouldParseForStatement()
    {
        //Arrange
        var content = "for(var i =0 in range(10)) { } ";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ForeachStatement>(0);
    }
}