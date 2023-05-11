using System.Dynamic;

namespace SL.Parser.Parsing.AST;

public class EmptyBlockStatement : Statement
{
    
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        return model;
    }
}