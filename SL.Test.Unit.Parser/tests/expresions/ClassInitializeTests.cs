using SL.Parser.Models;
using SL.Parser.Models.Expressions;
using SL.Parser.Models.Statements;

namespace SL.Test.Unit.Parser.tests.expresions;

public class ClassInitializeTests : ParserTestBase
{
    [Test]
    public async Task ShouldInitializeClass()
    {
        //Arrange
        var content = "new User(2,3);";

        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(0, x =>
            {
                x.HasChild<ClassInitializeExpressionStatement>(0, assertion =>
                {
                    assertion.Has(e =>
                    {
                        Assert.That(e.ClassInitializer.FunctionName, Is.EquivalentTo("User"));
                        Assert.That(e.ClassInitializer.Paramteters.Count, Is.EqualTo(2));
                        Assert.That(e.ClassInitializer.NextCall, Is.EqualTo(null));
                    });
                });
            });

    }
    
    
    [Test]
    public async Task ShouldInitializeClassAndAssign()
    {
        //Arrange
        var content = "var user = 0; user = new User(2,3);";

        
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ExpresionStatement>(2, x =>
            {
                x.Has(statement =>
                {
                    NodeAssert.Assert<AssigmentExpression>(statement.Expression).Has(expression =>
                    {
                        NodeAssert.Assert<ClassInitializeExpressionStatement>(expression.Right).Has(
                            e =>
                            {
                                Assert.That(e.ClassInitializer.FunctionName, Is.EquivalentTo("User"));
                                Assert.That(e.ClassInitializer.Paramteters.Count, Is.EqualTo(2));
                                Assert.That(e.ClassInitializer.NextCall, Is.EqualTo(null));
                            });
                    });
                });

            });
    }

}