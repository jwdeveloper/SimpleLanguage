using SL.Parser.Common;

namespace SL.Parser.Parsing.AST.Expressions;

public class AssigmentExpression : BinaryExpression
{
    public AssigmentExpression(Token operation, Expression left, Expression right) : base(operation, left, right)
    {
        
    }
}