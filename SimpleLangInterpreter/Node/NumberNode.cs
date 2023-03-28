﻿namespace SimpleLangInterpreter.Node;

public class NumberNode : ExpresionSyntax
{
    public NumberNode(SyntaxToken token) : base(token)
    {
        
    }


    public override object execute()
    {
        return double.Parse(token.Symbol);
    }

    public override string getValue()
    {
        return token.Symbol;
    }
    
    public override IEnumerable<SyntaxNode> getChildren()
    {
       return Enumerable.Empty<SyntaxNode>();
    }

    public override NodeType getNoteType()
    {
        return NodeType.NumberNode;
    }
}