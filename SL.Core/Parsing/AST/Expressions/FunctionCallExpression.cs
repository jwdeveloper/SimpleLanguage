namespace SL.Core.Parsing.AST.Expressions;

public class FunctionCallExpression : Expression
{
    
    private IdentifierLiteral _functionName;

    private List<Expression> _paramteters;

    private FunctionCallExpression _nextCall;
    
    
    public FunctionCallExpression(IdentifierLiteral functionName,
        List<Expression> paramteters,
        FunctionCallExpression nextCall)
    {
        this._functionName = functionName;
        this._paramteters = paramteters;
        this._nextCall = nextCall;
    }


}