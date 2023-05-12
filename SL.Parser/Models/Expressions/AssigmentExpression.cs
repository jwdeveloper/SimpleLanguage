using SL.Tokenizer.Models;

namespace SL.Parser.Models.Expressions;

public class AssigmentExpression : BinaryExpression
{
    public AssigmentExpression(Token operation, Expression left, Expression right) : base(operation, left, right)
    {
        
    }
}