using System.Dynamic;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.AST;

public class WhileBlockStatement : Statement
{
    public Expression Condition { get; }

    public Statement Body { get; }

    public bool IsDoWhile { get; }
    
    public WhileBlockStatement(Expression condition, Statement body, bool isDoWhile)
    {
        Body = body;
        Condition = condition;
        IsDoWhile = isDoWhile;
    }

  
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.condition = Condition.GetModel();
        model.body = Body.GetModel();
        model.isDoWhile = IsDoWhile;
        return model;
    }
}