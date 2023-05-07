using System.Globalization;
using SL.Core.Common;
using SL.Core.Parsing.AST;
using SL.Core.Parsing.AST.Expressions;

namespace SL.Core.Parsing;

public class NodeFactory
{
    public Literal LiteralString(Token token)
    {
        return new TextLiteral(token.Value);
    }

    public Literal LiteralNumber(Token value)
    {
        return new NumericLiteral(float.Parse(value.Value, CultureInfo.InvariantCulture));
    }

    public Literal LiteralBool(Token token)
    {
        return new BoolLiteral(bool.Parse(token.Value));
    }

    public IdentifierLiteral IdentifierLiteral(Token value)
    {
        return new IdentifierLiteral(value.Value);
    }

    public BlockStatement BlockStatement(List<Statement> statements)
    {
        return new BlockStatement(statements, string.Empty);
    }

    public ExpresionStatement ExpressionStatement(Expression expressions)
    {
        return new ExpresionStatement(expressions);
    }

    public FunctionDeclarationStatement FunctionStatement(
        IdentifierLiteral functionName,
        IdentifierLiteral functionType,
        List<ParameterStatement> parameterStatements,
        BlockStatement body)
    {
        return new FunctionDeclarationStatement(functionName, functionType, parameterStatements, body);
    }


    public FunctionCallExpression FunctionCallExpression(
        IdentifierLiteral functionName,
        List<Expression> paramteters,
        FunctionCallExpression nextCall)
    {
        return new FunctionCallExpression(functionName, paramteters, nextCall);
    }


    public IfStatement IfStatement(Expression condition, Statement body, Statement elseBody)
    {
        return new IfStatement(condition, body, elseBody);
    }

    public WhileStatement WhileStatement(Expression condition, Statement body, bool isDoWhile)
    {
        return new WhileStatement(condition, body, isDoWhile);
    }

    public ForStatement ForStatement(
        VariableStatement declaration,
        Expression condition,
        Expression assigment,
        Statement body,
        bool isForeach)
    {
        return new ForStatement(declaration, condition, assigment, body, isForeach);
    }

    public ForeachStatement ForeachStatement(
        VariableStatement declaration,
        Expression iterator,
        Statement body)
    {
        return new ForeachStatement(declaration, iterator, body);
    }


    public BinaryExpression BinaryExpression(Token operation, Expression left, Expression right)
    {
        return new BinaryExpression(operation, left, right);
    }

    public VariableStatement VariableDeclarations(Token variableType,
        List<VariableDeclarationStatement> variableStatements)
    {
        return new VariableStatement(variableType, variableStatements);
    }

    public EmptyStatement EmptyStatement()
    {
        return new EmptyStatement();
    }
}