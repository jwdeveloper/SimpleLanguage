using System.Dynamic;
using SL.Parser.Common;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.AST;

public class VariableStatement : Statement
{
    private readonly Token _variableTypeToken;
    public List<VariableDeclarationStatement> VariableDeclarations { get; }
    public string VariableType => _variableTypeToken.Value;
    
    
    //TODO Remove 
    public VariableStatement(Token variableTypeToken, List<VariableDeclarationStatement> variableDeclarations)
    {
        _variableTypeToken = variableTypeToken;
        VariableDeclarations = variableDeclarations;
    }
    
    public VariableStatement(IdentifierLiteral variableTypeToken, List<VariableDeclarationStatement> variableDeclarations)
    {
        _variableTypeToken = new Token(TokenType.DOT, variableTypeToken.Value.ToString(),null);
        VariableDeclarations = variableDeclarations;
    }

    public override IEnumerable<Node> Children()
    {
        return VariableDeclarations;
    }
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.variableType =VariableType;
        var parametersModel = new List<dynamic>();
        foreach(var paramerter in VariableDeclarations)
        {
            parametersModel.Add(paramerter.GetModel());
        }
        model.declarations =parametersModel;
        return model;
    }
}