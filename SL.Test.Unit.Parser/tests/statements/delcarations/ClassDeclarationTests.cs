using SL.Parser.Models;
using SL.Parser.Models.Statements.Declarations;

namespace SL.Test.Unit.Parser.tests.statements.delcarations;

public class ClassDeclarationTests : ParserTestBase
{
    [Test]
    public async Task ShouldDeclareSimpleClass()
    {
        //Arrange
        var content = "class User {}";

        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ClassDeclarationStatement>(0,
                assertion =>
                {
                    assertion.Has(statement =>
                    {
                        Assert.That("User", Is.EqualTo(statement.ClassNameIdentifier.IdentifierName));
                    });
                });
    }


    [Test]
    public async Task ShouldDeclareSimpleClassWithDefaultConsturctor()
    {
        //Arrange
        var content = "class User(name,age) {}";

        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ClassDeclarationStatement>(0, assertion =>
            {
                assertion.Has(statement =>
                {
                    Assert.That(statement.ClassNameIdentifier.IdentifierName, Is.EqualTo("User"));
                    Assert.That(statement.ClassConsturctors.Count, Is.EqualTo(1));
                    NodeAssert.Assert<FunctionDeclarationStatement>(statement.ClassConsturctors[0])
                        .Has(constuctor =>
                        {
                         
                          Assert.That(constuctor.FunctionName, Is.EqualTo("User"));
                          Assert.That(constuctor.FunctionType.IdentifierName, Is.EqualTo("constructor#2"));
                          Assert.That(constuctor.ParameterStatements.Count, Is.EqualTo(2));
                        });
                });
            });
    }
    
    
    [Test]
    public async Task ShouldDeclareClassWithMethods()
    {
        //Arrange
        var content = @"class User(name,age) 
                       {
                           text lastName;                           

                           function getAge()
                           {
                             return this.age;
                           }

                           function getName()
                           {
                              return this.name;
                           }
                       }";

        //Act
        var program = await CreateProgramTree(content);

        //Assert
        NodeAssert.Assert<SlProgram>(program)
            .HasChild<ClassDeclarationStatement>(0, assertion =>
            {
                assertion.Has(statement =>
                {
                    Assert.That(statement.ClassFunctions.Count, Is.EqualTo(2));
                    Assert.That(statement.ClassFields.Count, Is.EqualTo(3));
                    Assert.That(statement.ClassNameIdentifier.IdentifierName, Is.EqualTo("User"));
                    Assert.That(statement.ClassConsturctors.Count, Is.EqualTo(1));
                    NodeAssert.Assert<FunctionDeclarationStatement>(statement.ClassConsturctors[0])
                        .Has(constuctor =>
                        {
                         
                            Assert.That(constuctor.FunctionName, Is.EqualTo("User"));
                            Assert.That(constuctor.FunctionType.IdentifierName, Is.EqualTo("constructor#2"));
                            Assert.That(constuctor.ParameterStatements.Count, Is.EqualTo(2));
                        });
                });
            });
    }
}