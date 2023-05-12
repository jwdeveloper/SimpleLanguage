using System.Dynamic;

namespace SL.Parser.Models.Statements.Blocks;

public class ForeachStatement: ForLoopStatement
{
    public VariableStatement Declaration { get; }
    public Expression Iterator { get; }
    public Statement Body { get; }

    public ForeachStatement(
        VariableStatement declaration,
        Expression iterator,
        Statement body)
    {
        this.Declaration = declaration;
        this.Iterator = iterator;
        this.Body = body;
    }
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.declaration = Declaration.GetModel();
        model.iterator = Iterator.GetModel();
        model.body = Body.GetModel();
        return model;
    }
}