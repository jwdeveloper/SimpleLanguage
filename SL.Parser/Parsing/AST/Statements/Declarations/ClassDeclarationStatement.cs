using System.Dynamic;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.AST;

public class ClassDeclarationStatement : Statement
{
    public IdentifierLiteral ClassNameIdentifier { get; }
    
    public List<VariableStatement> ClassFields { get; }
    
    public List<FunctionDeclarationStatement> ClassFunctions { get; }
    
    public List<FunctionDeclarationStatement> ClassConsturctors { get; }

    public ClassDeclarationStatement(IdentifierLiteral classNameIdentifier,
        List<VariableStatement> fields,
        List<FunctionDeclarationStatement> consturctors,
        List<FunctionDeclarationStatement> functions)
    {
        ClassNameIdentifier = classNameIdentifier;
        ClassFields = fields;
        ClassFunctions = functions;
        ClassConsturctors = consturctors;
    }

    
    public ClassDeclarationStatement(IdentifierLiteral classNameIdentifier,
        List<VariableStatement> fields)
    {
        ClassNameIdentifier = classNameIdentifier;
        ClassFields = fields;
        ClassFunctions = new List<FunctionDeclarationStatement>();
        ClassConsturctors = new List<FunctionDeclarationStatement>();
    }
    
    public ClassDeclarationStatement(IdentifierLiteral classNameIdentifier,
        List<VariableStatement> fields,
        List<FunctionDeclarationStatement> consturctors)
    {
        ClassNameIdentifier = classNameIdentifier;
        ClassFields = fields;
        ClassConsturctors = consturctors;
        ClassFunctions = new List<FunctionDeclarationStatement>();
    }
    
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.className = ClassNameIdentifier.GetModel();
        var fieldsModels = new List<dynamic>();
        foreach(var paramerter in ClassFields)
        {
            fieldsModels.Add(paramerter.GetModel());
        }
        model.fields = fieldsModels;
        
        
        var consturctors = new List<dynamic>();
        foreach(var paramerter in ClassConsturctors)
        {
            consturctors.Add(paramerter.GetModel());
        }
        model.consturctors = consturctors;
        
        var functionsModels = new List<dynamic>();
        foreach(var paramerter in ClassFunctions)
        {
            functionsModels.Add(paramerter.GetModel());
        }
        model.functions = functionsModels;
        
        
        
     
        return model;
        

    }
}