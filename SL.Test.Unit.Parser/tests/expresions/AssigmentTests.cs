using SL.Parser.Models;
using SL.Parser.Models.Expressions;
using SL.Parser.Models.Literals;
using SL.Parser.Models.Statements;
using SL.Tokenizer.Exceptions;

namespace SL.Test.Unit.Parser.tests.expresions;

public class AssigmentTests : ParserTestBase
{
    [Test]
    public async Task ShouldHandleAssigment()
    {
        //Arrange
        var content = "myValue = 12 + 3;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<AssigmentExpression>(0, nodeAssertion =>
                {
                    nodeAssertion.Has(expression =>
                    {
                        NodeAssert.Assert<IdentifierLiteral>(expression.Left);
                        Assert.That(expression.Operator, Is.EqualTo("="));
                        NodeAssert.Assert<BinaryExpression>(expression.Right);
                    });
                    
                });
            });
    }
    
    [Test]
    public async Task ShouldHandleAssigmentChain()
    {
        //Arrange
        var content = "x = y = 12;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<AssigmentExpression>(0, nodeAssertion =>
                {
                    nodeAssertion.Has(expression =>
                    {
                        NodeAssert.Assert<IdentifierLiteral>(expression.Left).Has(e => Assert.That(e.Value, Is.EqualTo("x")));
                        Assert.That(expression.Operator, Is.EqualTo("="));
                        NodeAssert.Assert<AssigmentExpression>(expression.Right);
                    });
                    
                });
            });
    }
    
    [Test]
    public async Task ShouldHandleComplexAssigment()
    {
        //Arrange
        var content = "x += 12;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<AssigmentExpression>(0, nodeAssertion =>
                {
                    nodeAssertion.Has(expression =>
                    {
                        NodeAssert.Assert<IdentifierLiteral>(expression.Left).Has(e => Assert.That(e.Value, Is.EqualTo("x")));
                        Assert.That(expression.Operator, Is.EqualTo("+="));
                        NodeAssert.Assert<NumericLiteral>(expression.Right);
                    });
                    
                });
            });
    }
    
        
    [Test]
    public async Task ShouldThrowException()
    {
        //Arrange
        var content = "12 = 12;";
     
        //Act
        //Assert
        Assert.ThrowsAsync<SyntaxException>( async () =>
        {
            await CreateProgramTree(content);
        });
    }
    
}