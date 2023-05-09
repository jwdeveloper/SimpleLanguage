using System.Linq.Expressions;
using SL.Core.Api;
using SL.Core.Api.Exceptions;
using SL.Core.Common;
using SL.Core.Parsing.AST;
using SL.Core.Parsing.AST.Expressions;
using SL.Core.Parsing.AST.Statements;
using Expression = SL.Core.Parsing.AST.Expression;

namespace SL.Core.Parsing;

public class Parser
{
    private readonly NodeFactory _nodeFactory;
    private ITokenIterator? _tokenIterator;

    public Parser(NodeFactory nodeFactory)

    {
        _nodeFactory = nodeFactory;
        _tokenIterator = null;
    }

    public SlProgram Parse(ITokenIterator tokenIterator)
    {
        _tokenIterator = tokenIterator;
        return GetProgram();
    }


    private SlProgram GetProgram()
    {
        var statements = GetStatementList(TokenType.END_OF_FILE);
        return new SlProgram(statements);
    }


    /*
    * StatementList
    *  Statement | StatementList
    */
    private List<Statement> GetStatementList(TokenType stopLookAhead)
    {
        var statements = new List<Statement>();
        try
        {
            while (_tokenIterator.IsValid() && _tokenIterator.LookUp().Type != stopLookAhead)
            {
                var statement = GetStatement();
                statements.Add(statement);
            }
        }
        catch (EndOfParsingException e)
        {
            return statements;
        }

        return statements;
    }


    /*
   * Statement
   *  ExpressionStatement | BlockStatement | EmptyStatement
   *   
   */
    private Statement GetStatement()
    {
        var token = _tokenIterator.LookUp();
        switch (token.Type)
        {
            case TokenType.OPEN_BLOCK:
                return GetBlockStatement();
            case TokenType.END_OF_LINE:
                return GetEmptyStatement();
            case TokenType.OBJECT_TYPE:
                return GetVariableDeclarationStatement();
            case TokenType.KEYWORLD:
                if (token.Value == "if")
                    return GetIfStatement();
                if (token.Value is "while")
                    return GetWhileStatement();
                if (token.Value is "do")
                    return GetDoWhileStatement();
                if (token.Value is "for")
                    return GetForStatement();
                if (token.Value is "function")
                    return GetFunctionStatement();
                if (token.Value is "return")
                    return GetReturnStatement();
                break;
            default:
                return GetExpressionStatement();
        }

        throw new SyntaxException("Unexpected statement", token);
    }

    private ReturnStatement GetReturnStatement()
    {
        _tokenIterator.NextToken("return");
        var expression = _tokenIterator.LookUp().Type == TokenType.END_OF_LINE ? null : GetExpression();
        _tokenIterator.NextToken(TokenType.END_OF_LINE);
        return new ReturnStatement(expression);
    }

    private IfStatement GetIfStatement()
    {
        _tokenIterator.NextToken("if");
        _tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);
        var condition = GetExpression();
        _tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);

        var body = GetStatement();
        if (_tokenIterator.LookUp().Value != "else")
        {
            return _nodeFactory.IfStatement(condition, body, _nodeFactory.BlockStatement(new List<Statement>()));
        }

        _tokenIterator.NextToken("else");
        if (_tokenIterator.LookUp().Value == "if")
        {
            return _nodeFactory.IfStatement(condition, body, GetIfStatement());
        }

        return _nodeFactory.IfStatement(condition, body, GetStatement());
    }

    private WhileStatement GetWhileStatement()
    {
        _tokenIterator.NextToken("while");
        _tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);
        var condition = GetExpression();
        _tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);

        var body = GetStatement();
        return _nodeFactory.WhileStatement(condition, body, false);
    }

    private WhileStatement GetDoWhileStatement()
    {
        _tokenIterator.NextToken("do");
        var body = GetStatement();

        _tokenIterator.NextToken("while");
        _tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);
        var condition = GetExpression();
        _tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);

        return _nodeFactory.WhileStatement(condition, body, true);
    }


    private Statement GetForStatement()
    {
        _tokenIterator.NextToken("for");
        _tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);

        var declaration = _tokenIterator.LookUp().Type == TokenType.END_OF_LINE
            ? null
            : GetVariableDeclarationStatement();
        if (_tokenIterator.LookUp().Value == "in")
        {
            return GetForeachStatement(declaration);
        }

        _tokenIterator.NextToken(TokenType.END_OF_LINE);

        var condition = _tokenIterator.LookUp().Type == TokenType.END_OF_LINE ? null : GetExpression();
        _tokenIterator.NextToken(TokenType.END_OF_LINE);

        var assigment = _tokenIterator.LookUp().Type == TokenType.CLOSE_ARGUMENTS ? null : GetExpression();
        _tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);

        var body = GetStatement();
        return _nodeFactory.ForStatement(declaration, condition, assigment, body);
    }

    private ForeachStatement GetForeachStatement(VariableStatement declaration)
    {
        if (declaration == null)
        {
            throw new SyntaxException("Foreach must have variable declaration", _tokenIterator.CurrentToken());
        }

        _tokenIterator.NextToken("in");

        var iterator = GetExpression();
        _tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);

        var body = GetStatement();
        return _nodeFactory.ForeachStatement(declaration, iterator, body);
    }


    private VariableStatement GetVariableDeclarationStatement()
    {
        var objectType = _tokenIterator.NextToken(TokenType.OBJECT_TYPE);
        var declaration = GetVariableDecrlarationStatementList();
        return _nodeFactory.VariableDeclarations(objectType, declaration);
    }

    private List<VariableDeclarationStatement> GetVariableDecrlarationStatementList()
    {
        var declarations = new List<VariableDeclarationStatement>();
        do
        {
            if (_tokenIterator.LookUp().Type == TokenType.COMMA)
            {
                _tokenIterator.NextToken(TokenType.COMMA);
            }

            declarations.Add(GetVariableDecrlaration());
        } while (_tokenIterator.LookUp().Type == TokenType.COMMA);


        return declarations;
    }

    private VariableDeclarationStatement GetVariableDecrlaration()
    {
        var identifier = GetIdentifier();
        if (_tokenIterator.LookUp().Type == TokenType.END_OF_FILE)
        {
            return new VariableDeclarationStatement(identifier, null);
        }

        if (_tokenIterator.LookUp().Type == TokenType.ASSIGMENT)
        {
            _tokenIterator.NextToken(TokenType.ASSIGMENT);
            var assigment = GetAssigmentExpression();
            return new VariableDeclarationStatement(identifier, assigment);
        }


        return new VariableDeclarationStatement(identifier, null);
    }


    /*
    * EmptyStatement
    *  ';'
    *   
    */
    private Statement GetEmptyStatement()
    {
        _tokenIterator.NextToken(TokenType.END_OF_LINE);
        return _nodeFactory.EmptyStatement();
    }


    /*
    * BlockStatement
    *  '{' BlockBody '}'
    *   
    */
    private BlockStatement GetBlockStatement()
    {
        _tokenIterator.NextToken(TokenType.OPEN_BLOCK);

        var nextToken = _tokenIterator.LookUp();
        var body = nextToken.Type != TokenType.CLOSE_BLOCK
            ? GetStatementList(TokenType.CLOSE_BLOCK)
            : new List<Statement>();

        _tokenIterator.NextToken(TokenType.CLOSE_BLOCK);

        return _nodeFactory.BlockStatement(body);
    }

    /*
     *  function [optional]number GetNumbers([optional]number age, [optional]text name)
     *  {
     *   }
     */

    private FunctionDeclarationStatement GetFunctionStatement()
    {
        _tokenIterator.NextToken("function");

        var functionType = _tokenIterator.LookUp().Type == TokenType.OBJECT_TYPE
            ? _nodeFactory.IdentifierLiteral(_tokenIterator.NextToken(TokenType.OBJECT_TYPE))
            : null;

        var functionName = GetIdentifier();
        var parameters = GetParameters();
        var body = GetBlockStatement();

        return _nodeFactory.FunctionStatement(functionName, functionType, parameters, body);
    }

    private List<ParameterStatement> GetParameters()
    {
        var listParameter = new List<ParameterStatement>();
        _tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);

        while (_tokenIterator.LookUp().Type != TokenType.CLOSE_ARGUMENTS)
        {
            var parameterType = _tokenIterator.LookUp().Type == TokenType.OBJECT_TYPE
                ? _nodeFactory.IdentifierLiteral(_tokenIterator.NextToken(TokenType.OBJECT_TYPE))
                : null;

            var parameterName = GetIdentifier();

            var parameter = new ParameterStatement(parameterType, parameterName);
            listParameter.Add(parameter);

            if (_tokenIterator.LookUp().Type == TokenType.COMMA)
            {
                _tokenIterator.NextToken(TokenType.COMMA);
                continue;
            }

            break;
        }

        _tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);
        return listParameter;
    }

    /*
    * ExpressionStatement
    *  Expression ';'
    */
    private ExpresionStatement GetExpressionStatement()
    {
        var expression = GetExpression();
        _tokenIterator.NextToken(TokenType.END_OF_LINE);
        return _nodeFactory.ExpressionStatement(expression);
    }


    /*
     * Expression
     *  Literal 
     */
    private Expression GetExpression()
    {
        return GetAssigmentExpression();
    }


    private Expression GetLogicalExpression()
    {
        return GetGenericExpression(TokenType.LOGICAL_OPERATOR, GetEqualityExpression);
    }

    private Expression GetEqualityExpression()
    {
        return GetGenericExpression(TokenType.EQUALITY_OPREATOR, GetAddativeExpression);
    }
    /*
     * AddativeExpression
      *  Literal | AddativeExpression Operator Literal
      */

    private Expression GetAddativeExpression()
    {
        return GetGenericExpression(TokenType.BINARY_ADDATIVE_OPERATOR, GetMulitplicationExpression);
    }
    /*
     * MulitplicationExpression
        *  Literal | MulitplicationExpression Operator Literal
      */

    private Expression GetMulitplicationExpression()
    {
        return GetGenericExpression(TokenType.BINARY_MULTIPLICATION_OPERATOR, GetPrimaryExpression);
    }


    /*
     * AssigmentExpression
     *   AddativeExpression |
     *   LeftSideExpression AssigmentOperator AssigmentExpression
     */
    private Expression GetAssigmentExpression()
    {
        var left = GetLogicalExpression();
        if (!isAssigmentType(_tokenIterator.LookUp()))
        {
            return left;
        }

        if (!(left is IdentifierLiteral))
        {
            throw new SyntaxException($"to assigned type must be of type {TokenType.IDENTIFIER}",
                _tokenIterator.CurrentToken());
        }

        var assigmentToken = _tokenIterator.NextToken(TokenType.ASSIGMENT, TokenType.COMPLEX_ASSIGMENT);

        return new AssigmentExpression(assigmentToken, left, GetAssigmentExpression());
    }


    private bool isAssigmentType(Token token)
    {
        return token.Type == TokenType.ASSIGMENT || token.Type == TokenType.COMPLEX_ASSIGMENT;
    }


    private Expression GetGenericExpression(TokenType tokenType, Func<Expression> finderFunction)
    {
        var left = finderFunction.Invoke();

        while (_tokenIterator.LookUp().Type == tokenType)
        {
            var binaryOperator = _tokenIterator.NextToken(tokenType);
            var right = finderFunction.Invoke();

            left = _nodeFactory.BinaryExpression(binaryOperator, left, right);
        }

        return left;
    }


    /*
    * PrimaryExpression
    *  Literal | PerentesisExpressions
    */
    private Expression GetPrimaryExpression()
    {
        var token = _tokenIterator.LookUp();
        switch (token.Type)
        {
            case TokenType.OPEN_ARGUMETNS:
                return GetParentesisExpression();
            case TokenType.IDENTIFIER:
                return GetIdentifierExpression();
            default:
                return GetLiteral();
        }
    }


    private Expression GetIdentifierExpression()
    {
        var token = _tokenIterator.NextToken(TokenType.IDENTIFIER);
        var identifier = _nodeFactory.IdentifierLiteral(token);
        if (_tokenIterator.LookUp().Type != TokenType.OPEN_ARGUMETNS)
        {
            return identifier;
        }

        return GetFunctionCall(identifier);
    }


    private Expression GetFunctionCall(IdentifierLiteral identifierLiteral)
    {
        var parameters = GetFunctionCallParameters();

        if (_tokenIterator.LookUp().Type == TokenType.DOT)
        {
            _tokenIterator.NextToken(TokenType.DOT);
            return _nodeFactory.FunctionCallExpression(identifierLiteral, parameters, GetIdentifierExpression());
        }


        return _nodeFactory.FunctionCallExpression(identifierLiteral, parameters, null);
    }


    private List<Expression> GetFunctionCallParameters()
    {
        var expressions = new List<Expression>();
        _tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);
        if (_tokenIterator.LookUp().Type == TokenType.CLOSE_ARGUMENTS)
        {
            _tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);
            return expressions;
        }

        do
        {
            expressions.Add(GetExpression());
        } while (_tokenIterator.LookUp().Type == TokenType.COMMA && _tokenIterator.NextToken(TokenType.COMMA) != null);

        _tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);
        return expressions;
    }

    /*
     * PerentesisExpressions
     *  '(' Expression ')'
     */
    private Expression GetParentesisExpression()
    {
        _tokenIterator.NextToken(TokenType.OPEN_ARGUMETNS);
        var expression = GetExpression();
        _tokenIterator.NextToken(TokenType.CLOSE_ARGUMENTS);
        return expression;
    }


    private IdentifierLiteral GetIdentifier()
    {
        if (_tokenIterator.LookUp().Type != TokenType.IDENTIFIER)
        {
            throw new SyntaxException("Expected identifer", _tokenIterator.LookUp());
        }

        var token = _tokenIterator.NextToken();
        return _nodeFactory.IdentifierLiteral(token);
    }

    /*
     * Literal
     *   BoolLiteral | StringLiteral | NumberLiteral
     */
    private Literal GetLiteral()
    {
        var token = _tokenIterator.NextToken();
        switch (token.Type)
        {
            case TokenType.BOOL:
                return _nodeFactory.LiteralBool(token);
            case TokenType.STRING:
                return _nodeFactory.LiteralString(token);
            case TokenType.NUMBER:
                return _nodeFactory.LiteralNumber(token);
        }

        throw new SyntaxException("Unexpected literal", token);
    }
}