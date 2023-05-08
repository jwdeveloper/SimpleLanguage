using System.Dynamic;
using SL.Core.Parsing.AST.Expressions;

namespace SL.Core.Parsing.AST;

public class ParameterStatement : Node
{
    private IdentifierLiteral paramterType;

    private IdentifierLiteral parameterName;


    public ParameterStatement(IdentifierLiteral paramterType, IdentifierLiteral parameterName)
    {
        this.paramterType = paramterType;
        this.parameterName = parameterName;
    }
    
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.parameterType = paramterType?.GetModel();
        model.parameterName = parameterName?.GetModel();
        return model;
    }
}