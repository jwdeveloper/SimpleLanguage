namespace SL.Core.Parsing.AST.Expressions;

public class ExpresionStatement : Statement
{

    public Expression Expression { get; }

    public ExpresionStatement(Expression expression)
    {
      Expression = expression;
    }


    public override IEnumerable<Node> Children()
    {
        return  new[] { Expression };
    }
}