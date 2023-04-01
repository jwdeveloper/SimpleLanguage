using System.Linq.Expressions;

namespace SimpleLangInterpreter.Node;

public enum NodeType
{
    Undefined, Expression, Program, NumberNode, BinaryExpression, BoolNode,StringNode,VariableNode
}