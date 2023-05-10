using System.Dynamic;

namespace SL.Parser.Parsing.AST.Statements;

public class ReturnStatement : Statement
{

    public Expression? ReturnExpression { get; }

    public bool HasReturnExpression => ReturnExpression != null;

    public ReturnStatement(Expression? expression)
    {
        ReturnExpression = expression;
    }
    
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.expression = ReturnExpression?.GetModel();
        return model;
    }
}