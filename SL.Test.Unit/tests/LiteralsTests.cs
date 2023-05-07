using SL.Core.Parsing.AST;
using SL.Core.Parsing.AST.Expressions;

namespace SL.Test.Unit;

public class LiteralsTests : ParserTestBase
{
    [Test]
    public async Task ShouldParseLiterals()
    {
        //Arrange
        var content = " 1.23; \"hello world\"; true; myVariable;";
     
        //Act
        var program = await CreateProgram(content);

        //Assert
        NodeAssert.Assert<Program>(program)
            .HasChildrenCount(4)
            .HasChild<ExpresionStatement>(0,
                e =>
                {
                    e.HasChild<NumericLiteral>(0,
                        v => { v.Has(literal => { Assert.That(literal.Value, Is.EqualTo(1.23f)); }); });
                })
            .HasChild<ExpresionStatement>(1,
                e =>
                {
                    e.HasChild<TextLiteral>(0,
                        v => { v.Has(literal => { Assert.That(literal.Value, Is.EqualTo("hello world")); }); });
                })
            .HasChild<ExpresionStatement>(2,
                e =>
                {
                    e.HasChild<BoolLiteral>(0,
                        v => { v.Has(literal => { Assert.That(literal.Value, Is.EqualTo(true)); }); });
                }).
            HasChild<ExpresionStatement>(3,
                e =>
                {
                    e.HasChild<IdentifierLiteral>(0,
                        v => { v.Has(literal => { Assert.That(literal.Value, Is.EqualTo("myVariable")); }); });
                });
    }
}