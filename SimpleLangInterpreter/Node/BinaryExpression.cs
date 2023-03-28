namespace SimpleLangInterpreter.Node;

public class BinaryExpression : ExpresionSyntax
{

    private readonly ExpresionSyntax left;
    private readonly ExpresionSyntax righ;
    
    public BinaryExpression(SyntaxToken operation,ExpresionSyntax left, ExpresionSyntax right) : base(operation)
    {
        this.left = left;
        this.righ = right;
    }


    
    public override IEnumerable<SyntaxNode> getChildren()
    {
      return  new List<SyntaxNode>(){left,token, righ};
    }


    public override NodeType getNoteType()
    {
        return NodeType.BinaryExpression;
    }

    public override object execute()
    {
        var leftDecimal = (double)left.execute();
        var rightDecimal = (double)righ.execute();

        object result = 0;
        switch (token.Symbol)
        {
            case "+":
                result = leftDecimal + rightDecimal;
                break;
            case "-":
                result = leftDecimal - rightDecimal;
                break;
            case "*":
                result = leftDecimal * rightDecimal;
                break;
            case "/":
                result = leftDecimal / rightDecimal;
                break;
            case "^":
                result = Math.Pow(leftDecimal, rightDecimal);
                break;
            case "==":
                result = leftDecimal == rightDecimal;
                break;
            case ">=":
                result = leftDecimal >= rightDecimal ;
                break;
            case "<=":
                result = leftDecimal <= rightDecimal;
                break;
            case ">":
                result = leftDecimal > rightDecimal;
                break;
            case "<":
                result = leftDecimal < rightDecimal;
                break;
            case "and":
                result = leftDecimal > 0 && rightDecimal > 0;
                break;
            case "or":
                result = leftDecimal > 0 || rightDecimal > 0;
                break;
        }
        return result;
    }
}