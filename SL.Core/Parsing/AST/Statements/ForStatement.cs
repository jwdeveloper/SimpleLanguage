using System.Dynamic;

namespace SL.Core.Parsing.AST;

public class ForStatement : Statement
{
    public VariableStatement? Declaration { get; }
    public Expression? Condition { get; }
    public Expression? Assigment { get; }
    public Statement Body { get; }

    public bool HasDeclaration => Declaration != null;
    public bool HasCondition => Condition != null;
    public bool HasAssigment => Assigment != null;

    public ForStatement(
        VariableStatement declaration,
        Expression condition, 
        Expression assigment,
        Statement body) 
     
    {
        this.Declaration = declaration;
        this.Condition = condition;
        this.Assigment = assigment;
        this.Body = body;
        
    }



    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.declaration = Declaration?.GetModel();
        model.iterator = Condition?.GetModel();
        model.assigment = Assigment?.GetModel();
        model.body = Body.GetModel();
        return model;
    }

}