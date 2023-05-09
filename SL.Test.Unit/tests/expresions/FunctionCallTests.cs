using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Test.Unit.expresions;

public class FunctionCallTests : ParserTestBase
{
    [Test]
    public async Task ShouldCallFunction()
    {
        //Arrange
        var content = "getAge();";

        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<FunctionCallExpression>(0, e =>
                {
                    e.Has(e =>
                    {
                        Assert.That(e.FunctionName, Is.EquivalentTo("getAge"));
                        Assert.That(e.Paramteters.Count, Is.EqualTo(0));
                        Assert.That(e.NextCall, Is.EqualTo(null));
                    });
                });
            });
    }


    [Test]
    public async Task ShouldCallFunctionWithParameters()
    {
        //Arrange
        var content = "getAge(1,\"name\",true, userAge);";

        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<FunctionCallExpression>(0, e =>
                {
                    e.Has(e =>
                    {
                        Assert.That(e.FunctionName, Is.EquivalentTo("getAge"));
                        Assert.That(e.Paramteters.Count, Is.EqualTo(4));
                        Assert.That(e.NextCall, Is.EqualTo(null));
                    });
                });
            });
    }


    [Test]
    public async Task ShouldAssignFunctionToVariable()
    {
        //Arrange
        var content = "var age = getAge(1,\"name\",true, userAge);";

        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<VariableStatement>(0,
                assertion =>
                {
                    assertion.HasChild<VariableDeclarationStatement>(0,
                        e => { e.Has(e =>
                        {
                            NodeAssert.Assert<FunctionCallExpression>(e.AssigmentExpression);
                        }); });
                });
    }
    
    
    [Test]
    public async Task ShouldHandleChainedMethodCall()
    {
        //Arrange
        var content = "getAge(1).getName(\"Mark\");";

        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<FunctionCallExpression>(0, e =>
                {
                    e.Has(e =>
                    {
                        Assert.That(e.FunctionName, Is.EquivalentTo("getAge"));
                        Assert.That(e.Paramteters.Count, Is.EqualTo(1));
                        NodeAssert.Assert<FunctionCallExpression>(e.NextCall).Has(expression =>
                        {
                            Assert.That(expression.FunctionName, Is.EquivalentTo("getName"));
                            Assert.That(expression.Paramteters.Count, Is.EqualTo(1));
                        });
                    });
                });
            });
    }
    
    [Test]
    public async Task ShouldHandleChainedVariableCall()
    {
        //Arrange
        var content = "getAge(1).Name;";

        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, assertion =>
            {
                assertion.HasChild<FunctionCallExpression>(0, e =>
                {
                    e.Has(e =>
                    {
                        Assert.That(e.FunctionName, Is.EquivalentTo("getAge"));
                        Assert.That(e.Paramteters.Count, Is.EqualTo(1));
                        NodeAssert.Assert<IdentifierLiteral>(e.NextCall).Has(expression =>
                        {
                            Assert.That(expression.Value, Is.EqualTo("Name"));
                        });
                    });
                });
            });
    }
}