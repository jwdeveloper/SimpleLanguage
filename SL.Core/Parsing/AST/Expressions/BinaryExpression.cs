using System.Dynamic;
using SL.Core.Common;

namespace SL.Core.Parsing.AST.Expressions;

public class BinaryExpression : Expression
{
    private readonly Token _operation;
    public string Operator => _operation.Value;
    public Expression Left { get; }

    public Expression Right { get; }

    public BinaryExpression(Token operation, Expression left, Expression right)
    {
        _operation = operation;
        Left = left;
        Right = right;
    }

    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.left =Left;
        model.operatioin =Operator;
        model.right =Right;
        return model;
    }
}