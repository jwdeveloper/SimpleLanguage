using System.Dynamic;

namespace SL.Core.Parsing.AST.Statements;

public class ReturnStatement : Statement
{
    private Expression Expression;

    public ReturnStatement(Expression expression)
    {
        Expression = expression;
    }
    
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.expression = Expression?.GetModel();
        return model;
    }
}