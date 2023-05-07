namespace SL.Core.Parsing.AST;

public class ForStatement : Statement
{
    VariableStatement? declaration;
    Expression? condition;
    Expression? assigment;
    Statement body;
    private bool isForeach;


    public ForStatement(
        VariableStatement declaration,
        Expression condition, 
        Expression assigment,
        Statement body, 
        bool isForeach)
    {
        this.declaration = declaration;
        this.condition = condition;
        this.assigment = assigment;
        this.body = body;
        this.isForeach = isForeach;
    }
}