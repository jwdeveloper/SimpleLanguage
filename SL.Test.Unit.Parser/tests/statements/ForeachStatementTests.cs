using SL.Parser.Parsing.AST;

namespace SL.Test.Unit.Parser;

public class ForeachStatementTests : ParserTestBase
{
    [Test]
    public async Task ShouldParseForStatement()
    {
        //Arrange
        var content = "for(var i =0 in range(10)) { } ";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ForeachStatement>(0, assertion =>
            {
               
            });
    }
}