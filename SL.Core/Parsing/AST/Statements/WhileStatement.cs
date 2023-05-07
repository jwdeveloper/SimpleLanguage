namespace SL.Core.Parsing.AST;

public class WhileStatement : Statement
{
    public Expression Condition { get; }

    public Statement Body { get; }

    public bool IsDoWhile { get; }
    
    public WhileStatement(Expression condition, Statement body, bool isDoWhile)
    {
        Body = body;
        Condition = condition;
        IsDoWhile = isDoWhile;
    }

  
}