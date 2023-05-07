namespace SL.Core.Parsing.AST.Statements;

public class ReturnStatement : Statement
{
    private Expression Expression;

    public ReturnStatement(Expression expression)
    {
        Expression = expression;
    }
}