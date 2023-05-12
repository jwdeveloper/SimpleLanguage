using SL.Parser.Models;
using SL.Parser.Models.Literals;
using SL.Parser.Models.Statements;

namespace SL.Test.Unit.Parser.tests;

public class LiteralsTests : ParserTestBase
{
    [Test]
    public async Task ShouldParseLiterals()
    {
        //Arrange
        var content = " 1.23; \"hello world\"; true; myVariable;";
     
        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
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