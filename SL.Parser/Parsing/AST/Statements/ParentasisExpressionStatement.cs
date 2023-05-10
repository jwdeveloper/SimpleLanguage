namespace SL.Parser.Parsing.AST.Expressions;

public class ParentasisExpressionStatement : ExpresionStatement
{
    public ParentasisExpressionStatement(Expression expression) : base(expression)
    {
        
    }
    
    public static implicit operator Expression(ParentasisExpressionStatement d) => d.Expression;
}