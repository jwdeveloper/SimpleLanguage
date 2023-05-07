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
    
    
}