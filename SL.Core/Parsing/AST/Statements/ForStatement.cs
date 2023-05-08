using System.Dynamic;

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



    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.declaration = declaration?.GetModel();
        model.iterator = condition?.GetModel();
        model.assigment = assigment?.GetModel();
        model.body = body.GetModel();
        return model;
    }

}