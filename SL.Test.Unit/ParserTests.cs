using SL.Core.Lexing;
using SL.Core.Parsing;
using SL.Core.Parsing.AST;

namespace SL.Test.Unit;

public class ParserTests
{
    [Test]
    public void Test1()
    {

        var lexer = new LexerBuilder().Build();
        
        var builder = new ParserBuilder(lexer);

        builder.WithRule("siema", (factory, nodes) =>
        {
            return factory.Block(nodes, "Program");
        });
        
        builder.WithRule("expression = NUMBER", (factory, nodes) =>
        {
            return factory.ValueNumber(nodes[0]);
        });
        
        builder.WithRule("expression = NUMBER", (factory, nodes) =>
        {
            return factory.Statement(nodes);
        });
        
        builder.WithRule("expression = NUMBER", (factory, nodes) =>
        {
            return factory.Statement(nodes);
        });
        
        builder.WithRule("expression = expression BINARY_EXPRESSION expression", (factory, nodes) =>
        {
            return factory.BinaryExpression(nodes[1], (Expression)nodes[0], (Expression)nodes[2]);
        });
        
        var parser = builder.Build();

    }
}