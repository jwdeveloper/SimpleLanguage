using System.Dynamic;

namespace SL.Core.Parsing.AST;

public class EmptyStatement : Statement
{
    
    
    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        return model;
    }
}