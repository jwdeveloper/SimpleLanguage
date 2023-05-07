namespace SL.Core.Parsing.AST;

public class IfStatement : Statement
{
    public Expression Condition { get; }

    public Statement Body { get; }

    public Statement ElseBody { get; }
    
    public IfStatement(Expression condition, Statement body, Statement elseBody)
    {
        Body = body;
        ElseBody = elseBody;
        Condition = condition;
    }

 
}