using System.Dynamic;
using SL.Core.Common;

namespace SL.Core.Parsing.AST;

public class VariableStatement : Statement
{
    private readonly Token _variableTypeToken;
    private readonly List<VariableDeclarationStatement> _variableDeclarations;
    public string VariableType => _variableTypeToken.Value;
    
    public VariableStatement(Token variableTypeToken, List<VariableDeclarationStatement> variableDeclarations)
    {
        _variableTypeToken = variableTypeToken;
        _variableDeclarations = variableDeclarations;
    }

    public override IEnumerable<Node> Children()
    {
        return _variableDeclarations;
    }
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.variableType =VariableType;
        var parametersModel = new List<dynamic>();
        foreach(var paramerter in _variableDeclarations)
        {
            parametersModel.Add(paramerter.GetModel());
        }
        model.declarations =parametersModel;
        return model;
    }
}