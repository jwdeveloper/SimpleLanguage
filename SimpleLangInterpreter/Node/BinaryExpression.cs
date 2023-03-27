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

    public override string execute()
    {
        var leftValue = left.execute();
        var rightValue = righ.execute();
        if (!double.TryParse(leftValue, out var leftDecimal))
        {
            throw new Exception("Unable to parse left value " + left.getValue());
        }
        if (!double.TryParse(rightValue, out var rightDecimal))
        {
            throw new Exception("Unable to parse right value " + left.getValue());
        }

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
                result = leftDecimal == rightDecimal ? 1 : 0;
                break;
            case ">=":
                result = leftDecimal >= rightDecimal ? 1 : 0;
                break;
            case "<=":
                result = leftDecimal <= rightDecimal ? 1 : 0;
                break;
            case ">":
                result = leftDecimal > rightDecimal ? 1 : 0;
                break;
            case "<":
                result = leftDecimal < rightDecimal ? 1 : 0;
                break;
            case "and":
                result = leftDecimal > 0 && rightDecimal > 0;
                break;
            case "or":
                result = leftDecimal > 0 || rightDecimal > 0;
                break;
        }
        return result.ToString();
    }
}