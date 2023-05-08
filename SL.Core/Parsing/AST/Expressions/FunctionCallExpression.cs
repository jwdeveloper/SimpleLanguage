

using System.Dynamic;

namespace SL.Core.Parsing.AST.Expressions;

public class FunctionCallExpression : Expression
{
    
    public IdentifierLiteral FunctionName { get; }

    public List<Expression> Paramteters { get; }

    public Expression NextCall { get; }
    
    
    public FunctionCallExpression(IdentifierLiteral functionName,
        List<Expression> paramteters,
        Expression nextCall)
    {
        this.FunctionName = functionName;
        this.Paramteters = paramteters;
        this.NextCall = nextCall;
    }

    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.functionName =FunctionName.GetModel();
       
        var parametersModel = new List<dynamic>();
        foreach(var paramerter in Paramteters)
        {
            parametersModel.Add(paramerter.GetModel());
        }
        model.parameters =parametersModel;
        
        model.nextExpressionCall =NextCall?.GetModel();
        return model;
    }

}