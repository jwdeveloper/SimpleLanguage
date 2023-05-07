using SL.Core.Parsing.AST;
using SL.Core.Parsing.AST.Expressions;

namespace SL.Test.Unit;

public class IfStatementTests : ParserTestBase
{
    [Test]
    public async Task ShouldParseIfStatement()
    {
        //Arrange
        var content = "if(true) { }";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
            .HasChild<IfStatement>(0, assertion =>
            {
                assertion.Has(e =>
                {
                    NodeAssert.Assert<BoolLiteral>(e.Condition);
                    NodeAssert.Assert<BlockStatement>(e.Body);
                    NodeAssert.Assert<BlockStatement>(e.ElseBody);
                });
            });
    }
    
    [Test]
    public async Task ShouldParseIfElseStatement()
    {
        //Arrange
        var content = "if(true) { 1; 1.23; } else { 2; 5; 3;} ";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
            .HasChild<IfStatement>(0, assertion =>
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
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
            .HasChild<IfStatement>(0, assertion =>
            {
                assertion.Has(e =>
                {
                    NodeAssert.Assert<BoolLiteral>(e.Condition);
                    NodeAssert.Assert<BlockStatement>(e.Body).HasChildrenCount(2);
                    NodeAssert.Assert<IfStatement>(e.ElseBody);
                });
            });
    }
}