using SL.Parser.Parsing.AST;

namespace SL.Test.Unit.Parser;

public class NodeAssertion<T> where T : class
{
    private readonly Node _node;


    public NodeAssertion(Node node)
    {
        this._node = node;
        Assert.That(typeof(T), Is.EqualTo(_node.GetType()));
    }

    
    public NodeAssertion<T> Has(Action<T> assert)
    {
        var node = _node as T;
        assert.Invoke(node);
        return this;
    }
    
    public NodeAssertion<T> HasChild<X>(int index, Action<NodeAssertion<X>> onChild) where X : class
    {
        var childs = _node.Children().ToList();
        var child = childs[index];
        onChild.Invoke(new NodeAssertion<X>(child));
        return this;
    }
    
    public NodeAssertion<T> HasChild<X>(int index) where X : class
    {
        return HasChild<X>(index, e => { });
    }

    public NodeAssertion<T> HasChildrenCount(int count)
    {
        Assert.That(count, Is.EqualTo(_node.Children().Count()));
        return this;
    }
    
    public NodeAssertion<T> HasName(string name)
    {
        Assert.That(name, Is.EqualTo(_node.Name()));
        return this;
    }
    

}