namespace SL.Core.Parsing.AST;

public class ForeachStatement: Statement
{
    VariableStatement _declaration;
    Expression _iterator;
    Statement _body;

    public ForeachStatement(
        VariableStatement declaration,
        Expression iterator,
        Statement body)
    {
        this._declaration = declaration;
        this._iterator = iterator;
        this._body = body;
    }
}