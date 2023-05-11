using SL.Parser.Api;
using SL.Parser.Api.Exceptions;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing.Handlers.Statements.Declarations;

public class ClassDeclarationHandler : IParserHandler<ClassDeclarationStatement>
{
    public async Task<ClassDeclarationStatement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        tokenIterator.NextToken("class");
        var className = await parserFactory.CreateNode<IdentifierLiteral>("ignore");

        var defaultConstructorParameters = new List<ParameterStatement>();
        if (tokenIterator.LookUp(TokenType.OPEN_ARGUMETNS))
        {
            defaultConstructorParameters =  await parserFactory.CreateNode<List<ParameterStatement>>(tokenIterator, parserFactory);
        }

        var classStatement = CreateClassWithDefaultConstructor(className, defaultConstructorParameters);
        if (tokenIterator.LookUp(TokenType.END_OF_LINE))
        {
            tokenIterator.NextToken();
            return classStatement;
        }
        
        
        var body = await parserFactory.CreateNode<BlockStatement>();
        foreach(var statement in body.Statements)
        {
            if (statement is VariableStatement variableStatement)
            {
                classStatement.ClassFields.Add(variableStatement);
                continue;
            }
            if (statement is FunctionDeclarationStatement functionDeclaration)
            {
                if (functionDeclaration.FunctionName == className.IdentifierName)
                {
                    classStatement.ClassConsturctors.Add(functionDeclaration);
                    continue;
                }
                classStatement.ClassFunctions.Add(functionDeclaration);
                continue;
            }

            if (statement is EmptyBlockStatement)
            {
                continue;
            }
         
            throw new SyntaxException("Class body could only has fields and methods", tokenIterator.CurrentToken());
        }
        return classStatement;
    }


    private ClassDeclarationStatement CreateClassWithDefaultConstructor(IdentifierLiteral identifierLiteral, List<ParameterStatement> parameters)
    {

        var bodyStatements = new List<Statement>();
        var fields = new List<VariableStatement>();
        foreach(var parameter in parameters)
        {
            var parameterNameIdentyfier = parameter.parameterName;
            var parameterType = parameter.paramterType == null? new IdentifierLiteral("var") : parameter.paramterType;
            var variableStatement = new VariableStatement(parameterType, new List<VariableDeclarationStatement>()
            {
                new(parameterNameIdentyfier, null)
            });
            fields.Add(variableStatement);



            var variableName = new IdentifierLiteral("this",parameter.parameterName);
            var assigment = new AssigmentExpression(new Token(TokenType.ASSIGMENT,"=",new Position()), variableName, parameterNameIdentyfier);
            bodyStatements.Add(new ExpresionStatement(assigment));
        }

        var body = new BlockStatement(bodyStatements);
        
        var consturctor = new FunctionDeclarationStatement(
            identifierLiteral,
            new IdentifierLiteral("constructor#"+parameters.Count),
            parameters,
            body
            );
        
        return new ClassDeclarationStatement(identifierLiteral, fields,new List<FunctionDeclarationStatement>()
        {
            consturctor
        });
    }
}