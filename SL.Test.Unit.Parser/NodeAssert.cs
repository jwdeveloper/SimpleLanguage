using SL.Parser.Models;

namespace SL.Test.Unit.Parser;

public class NodeAssert
{
    public static NodeAssertion<X> Assert<X>(Node node) where X : class
    {
        return new NodeAssertion<X>(node);
    }
}