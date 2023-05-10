using System.Dynamic;

namespace SL.Parser.Parsing.AST.Expressions;

public class FunctionCallExpression : IdentifierLiteral
{
    private IdentifierLiteral _functionName { get; }

    public List<Expression> Paramteters { get; }

    public string FunctionName => _functionName.IdentifierName;

    public FunctionCallExpression(IdentifierLiteral functionName,
        List<Expression> paramteters,
        IdentifierLiteral nextCall = null) : base(functionName.IdentifierName, nextCall)
    {
        this._functionName = functionName;
        this.Paramteters = paramteters;
    }


    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.functionName = _functionName.GetModel();

        var parametersModel = new List<dynamic>();
        foreach (var paramerter in Paramteters)
        {
            parametersModel.Add(paramerter.GetModel());
        }

        model.parameters = parametersModel;

        model.nextExpressionCall = NextCall?.GetModel();
        return model;
    }
}