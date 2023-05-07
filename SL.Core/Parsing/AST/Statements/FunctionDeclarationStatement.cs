using SL.Core.Common;
using SL.Core.Parsing.AST.Expressions;

namespace SL.Core.Parsing.AST;

public class FunctionDeclarationStatement : Statement
{
    private IdentifierLiteral _functionName;
    private IdentifierLiteral _functionType;
    private List<ParameterStatement> _parameterStatements;
    private BlockStatement _body;
    
    public FunctionDeclarationStatement(
        IdentifierLiteral functionName,
        IdentifierLiteral functionType,
        List<ParameterStatement> parameterStatements,
        BlockStatement body)
    {
        this._functionName = functionName;
        this._functionType = functionType;
        _parameterStatements = parameterStatements;
        this._body = body;
    }

  
    
    
    
    

}