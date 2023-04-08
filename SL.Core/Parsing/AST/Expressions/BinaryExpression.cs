namespace SL.Core.Parsing.AST.Expressions;

public class BinaryExpression : Expression
{
    private Node _operation;
    private Expression _left;
    private Expression _right;

    public BinaryExpression(Node operation, Expression left, Expression right)
    {
        _operation = operation;
        _left = left;
        _right = right;
    }

    public override object Execute()
    {
        return null;
    }
}