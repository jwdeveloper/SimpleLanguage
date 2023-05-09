using System.Dynamic;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.AST;

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
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.declaration = _declaration.GetModel();
        model.iterator = _iterator.GetModel();
        model.body = _body.GetModel();
        return model;
    }
}