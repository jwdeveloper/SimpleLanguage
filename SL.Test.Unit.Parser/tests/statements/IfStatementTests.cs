using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Test.Unit.Parser;

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