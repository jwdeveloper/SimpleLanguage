using System.Dynamic;
using System.Text.Json.Serialization;

namespace SL.Core.Parsing.AST;

public abstract class Node
{
    public virtual IEnumerable<Node> Children()
    {
        return Enumerable.Empty<Node>();
    }
   
    public virtual string Name()
    {
        return GetType().Name;
    }
    
    
    
    public virtual dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();

        var models = new List<dynamic>();
        foreach(var childModel in Children())
        {
            var childModel2 = childModel.GetModel();
            models.Add(childModel2);
        }
        model.children = models;
        return model;
    }
}