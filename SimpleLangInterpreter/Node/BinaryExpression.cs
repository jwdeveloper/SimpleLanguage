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
        if (token.Symbol == "=")
        {

            if (left is CreateVariableExpersion createVariale)
            {
                return handleCreateVariable(createVariale);
            }
            if (left.token.TokenType == TokenType.LitteralToken)
            {
                return handleAssigmentVariable(left.token.Symbol);
            }
          
        }
        
        
        object leftResult = left.execute();
        object rightResult = righ.execute();
        if (leftResult is double && rightResult is double)
        {
            var leftDecimal = (double)leftResult;
            var rightDecimal = (double)rightResult;
            return handleNumber(leftDecimal, rightDecimal);
        }

        
        if (token.Symbol == "==")
        {
            if (leftResult is string s1 && rightResult is string s2)
            {
                return s1.Equals(s2);
            }
            
            return leftResult == rightResult;
        }


        if (leftResult is bool l1 && rightResult is bool l2) 
        {
            switch (token.Symbol)
            {
                case "and":
                   return l1  && l2;
                case "or":
                    return l1  || l2;
            }
        }
        
        
        switch (token.Symbol)
        {
            case "and":
                return leftResult !=null  && rightResult !=null;
            case "or":
                return leftResult!=null  || rightResult !=null;
        }

        return false;
    }

    public bool handleCreateVariable(CreateVariableExpersion createVariableExpersion)
    {
        var value = righ.execute();

        return evaluator.CreateVariable(createVariableExpersion.VariableName.Symbol, value);
    }
    
    public bool handleAssigmentVariable(string name)
    {
        var value = righ.execute();

        return evaluator.SetVariableValue(name, value);
    }
    
    public object handleNumber(double leftDecimal, double rightDecimal)
    {
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