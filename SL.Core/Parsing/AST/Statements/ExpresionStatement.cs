namespace SL.Core.Parsing.AST.Expressions;

public class ExpresionStatement : Statement
{

    private readonly Expression _expression;

    public ExpresionStatement(Expression expression)
    {
      _expression = expression;
    }


    public override IEnumerable<Node> Children()
    {
        return  new[] { _expression };
    }
}