using System.Dynamic;
using SL.Parser.Models.Literals;

namespace SL.Parser.Models.Statements;

public class ParameterStatement : Node
{
    public IdentifierLiteral? paramterType { get; }
    public IdentifierLiteral parameterName { get; }

    public bool HasParameterType => paramterType != null;

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