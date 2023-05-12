using System.Dynamic;
using SL.Parser.Models.Literals;

namespace SL.Parser.Models.Expressions;

public class FunctionCallExpression : IdentifierLiteral
{
    public IdentifierLiteral FunctionNameLiteral { get; }

    public List<Expression> Paramteters { get; }

    public string FunctionName => FunctionNameLiteral.IdentifierName;

    public FunctionCallExpression(IdentifierLiteral functionNameLiteral,
        List<Expression> paramteters,
        IdentifierLiteral nextCall = null) : base(functionNameLiteral.IdentifierName, nextCall)
    {
       FunctionNameLiteral = functionNameLiteral;
       Paramteters = paramteters;
    }


    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.functionName = FunctionNameLiteral.GetModel();

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