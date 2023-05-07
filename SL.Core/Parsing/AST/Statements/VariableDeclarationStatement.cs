using SL.Core.Parsing.AST.Expressions;

namespace SL.Core.Parsing.AST;

public class VariableDeclarationStatement : Statement
{
    private IdentifierLiteral IdentifierLiteral;
    private Expression AssigmentExpression;

    public VariableDeclarationStatement(IdentifierLiteral identifierLiteral, Expression assigmentExpression)
    {
        IdentifierLiteral = identifierLiteral;
        AssigmentExpression = assigmentExpression;
    }
}