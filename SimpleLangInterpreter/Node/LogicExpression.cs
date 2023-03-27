namespace SimpleLangInterpreter.Node;

public class LogicExpression : BinaryExpression
{
    public LogicExpression(SyntaxToken operation, ExpresionSyntax left, ExpresionSyntax right) : base(operation, left, right)
    {
        
    }


    public override string execute()
    {



        return "";
    }
}