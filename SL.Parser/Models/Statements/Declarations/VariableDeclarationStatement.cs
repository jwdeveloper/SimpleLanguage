using System.Dynamic;
using SL.Parser.Models.Literals;

namespace SL.Parser.Models.Statements.Declarations;

public class VariableDeclarationStatement : Statement
{
    public IdentifierLiteral IdentifierLiteral { get; }
    public Expression? AssigmentExpression{ get; }

    public VariableDeclarationStatement(IdentifierLiteral identifierLiteral, Expression? assigmentExpression)
    {
        IdentifierLiteral = identifierLiteral;
        AssigmentExpression = assigmentExpression;
    }

    public bool HasAssigmentExpression => AssigmentExpression != null;

    public string GetVariableDeclarationName => (string)IdentifierLiteral.Value;
    
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.identifier = IdentifierLiteral?.GetModel();
        model.assigmentExpression = AssigmentExpression?.GetModel();
        return model;
    }
}