using SL.Core.Parsing.AST;
using SL.Core.Parsing.AST.Expressions;
using ValueType = SL.Core.Parsing.AST.Expressions.ValueType;

namespace SL.Core.Parsing;

public class NodeFactory
{
    public ValueHolder ValueString(string value)
    {
        return new ValueHolder(value, ValueType.String);
    }

    public ValueHolder ValueNumber(float value)
    {
        return new ValueHolder(value, ValueType.Number);
    }

    public ValueHolder ValueBool(bool value)
    {
        return new ValueHolder(value, ValueType.Bool);
    }

    public ValueHolder ValueReference(string value)
    {
        return new ValueHolder(value, ValueType.Reference);
    }
    
    
    public Block Block(List<Statement> statements)
    {
        return new Block(statements, string.Empty);
    }
    
    public Block Block(List<Statement> statements, string name)
    {
        return new Block(statements, name);
    }
    
    public Block Statement(List<Statement> statements)
    {
        return null;
    }

    public BinaryExpression BinaryExpression(Node operation, Expression left, Expression right)
    {
        return new BinaryExpression(operation, left, right);
    }
}