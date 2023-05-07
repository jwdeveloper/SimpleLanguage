using SL.Core.Common;

namespace SL.Core.Parsing.AST.Expressions;

public class AssigmentExpression : BinaryExpression
{
    public AssigmentExpression(Token operation, Expression left, Expression right) : base(operation, left, right)
    {
        
    }
}