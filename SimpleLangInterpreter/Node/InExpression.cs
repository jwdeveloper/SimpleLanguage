namespace SimpleLangInterpreter.Node;

public class InExpression : BinaryExpression
{
    public InExpression(SyntaxToken operation, ExpresionSyntax left, ExpresionSyntax right) : base(operation, left, right)
    {
        
    }

    
    public CreateVariableExpersion LeftNode()
    {
        return left as CreateVariableExpersion;
    }

    public ExpresionSyntax RightNode()
    {
        return righ;
    }
}