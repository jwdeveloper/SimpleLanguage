using SL.Core.Parsing.AST.Expressions;

namespace SL.Core.Parsing.AST;

public class ParameterStatement
{
    private IdentifierLiteral paramterType;

    private IdentifierLiteral parameterName;


    public ParameterStatement(IdentifierLiteral paramterType, IdentifierLiteral parameterName)
    {
        this.paramterType = paramterType;
        this.parameterName = parameterName;
    }
}