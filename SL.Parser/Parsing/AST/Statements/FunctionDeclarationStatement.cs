using System.Dynamic;
using SL.Parser.Common;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.AST;

public class FunctionDeclarationStatement : Statement
{
    private IdentifierLiteral FunctionNameIdentifier { get; }
    public IdentifierLiteral? FunctionType { get; }
    public List<ParameterStatement> ParameterStatements { get; }
    public BlockStatement Body { get; }


    public string FunctionName => FunctionNameIdentifier.IdentifierName;

    public bool HasFunctionType => FunctionType != null; 
    
    public FunctionDeclarationStatement(
        IdentifierLiteral functionNameIdentifier,
        IdentifierLiteral functionType,
        List<ParameterStatement> parameterStatements,
        BlockStatement body)
    {
        FunctionNameIdentifier = functionNameIdentifier;
        FunctionType = functionType;
        ParameterStatements = parameterStatements;
        Body = body;
    }

  
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.functionType = FunctionType?.GetModel();
        model.functionName = FunctionNameIdentifier.GetModel();
        var parametersModel = new List<dynamic>();
        foreach(var paramerter in ParameterStatements)
        {
            parametersModel.Add(paramerter.GetModel());
        }
        
        model.parameters = parametersModel;
        model.body = Body.GetModel();
        return model;
    }
    
    

}