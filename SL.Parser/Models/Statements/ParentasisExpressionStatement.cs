namespace SL.Parser.Models.Statements;

public class ParentasisExpressionStatement : ExpresionStatement
{
    public ParentasisExpressionStatement(Expression expression) : base(expression)
    {
        
    }
    
    public static implicit operator Expression(ParentasisExpressionStatement d) => d.Expression;
}