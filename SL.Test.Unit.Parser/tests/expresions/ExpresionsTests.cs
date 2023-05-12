using SL.Parser.Models;
using SL.Parser.Models.Expressions;
using SL.Parser.Models.Literals;
using SL.Parser.Models.Statements;

namespace SL.Test.Unit.Parser.tests.expresions;

public class ExpresionsTests : ParserTestBase
{
        
    [Test]
    public async Task ShouldParseBinaryExpression()
    {
        //Arrange
        var content = "(2 + 2) * 4.3;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<BinaryExpression>(0, nodeAssertion =>
                {
                    nodeAssertion.Has(expression =>
                    {
                        Assert.That(expression.Operator, Is.EqualTo("*"));
                        NodeAssert.Assert<BinaryExpression>(expression.Left);
                        NodeAssert.Assert<NumericLiteral>(expression.Right);
                    });
                });
            });
    }
    
    [Test]
    public async Task ShouldHandleExpresionPersidence()
    {
        //Arrange
        var content = "2 * 2 + 4.3;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<BinaryExpression>(0, nodeAssertion =>
                {
                    nodeAssertion.Has(expression =>
                    {
                        Assert.That(expression.Operator, Is.EqualTo("+"));
                        NodeAssert.Assert<BinaryExpression>(expression.Left);
                        NodeAssert.Assert<NumericLiteral>(expression.Right);
                    });
                });
            });
    }
    
    [Test]
    public async Task ShouldHandleExpresionPersidence2()
    {
        //Arrange
        var content = "2 + 2 * 4.3;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<BinaryExpression>(0, nodeAssertion =>
                {
                    nodeAssertion.Has(expression =>
                    {
                        Assert.That(expression.Operator, Is.EqualTo("+"));
                        NodeAssert.Assert<BinaryExpression>(expression.Right);
                        NodeAssert.Assert<NumericLiteral>(expression.Left);
                    });
                });
            });
    }
    
    
    [DatapointSource]
    public string[] values = new[] { "==", "is", "!=",  ">", ">=", "<", "<=" };
    
    [Theory]
    public async Task EqualExpression(string operator_)
    {
        //Arrange
        var content = $"2 {operator_} 2;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<BinaryExpression>(0, nodeAssertion =>
                {
                    nodeAssertion.Has(expression =>
                    {
                        Assert.That(expression.Operator, Is.EqualTo(operator_));
                        NodeAssert.Assert<NumericLiteral>(expression.Right);
                        NodeAssert.Assert<NumericLiteral>(expression.Left);
                    });
                });
            });
    }
    
}