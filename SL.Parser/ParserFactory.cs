using SL.Parser.Api;
using SL.Parser.Parsing;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;
using SL.Parser.Parsing.AST.Statements;
using SL.Parser.Parsing.Handlers;
using SL.Parser.Parsing.Handlers.Expressions;
using SL.Parser.Parsing.Handlers.Expressions.Literals;
using SL.Parser.Parsing.Handlers.Statements;
using SL.Parser.Parsing.Handlers.Statements.Blocks;
using SL.Parser.Parsing.Handlers.Statements.Declarations;

namespace SL.Parser;

public class ParserFactory
{
    public  static Parsing.Parser CreateParser(ITokenIterator tokenIterator, CancellationToken ctx)
    {
        var parserBuilder = new ParserBuilder(tokenIterator, ctx);


        parserBuilder.WithParserHandler<SlProgram, SlProgramHandler>();
        
        //Statements 
        parserBuilder.WithParserHandler<List<Statement>, StatementListHandler>();
        parserBuilder.WithParserHandler<Statement, StatementHandler>();
        parserBuilder.WithParserHandler<ReturnStatement, ReturnHandler>();
        parserBuilder.WithParserHandler<ParentasisExpressionStatement, ParentasisExpressionHandler>();
        parserBuilder.WithParserHandler<ExpresionStatement, ExpresionStatementHandler>();
        parserBuilder.WithParserHandler<VariableStatement, VariableHandler>();
        
        //Statements declarations
        parserBuilder.WithParserHandler<FunctionDeclarationStatement, FunctionDeclarationHandler>();
        parserBuilder.WithParserHandler<VariableDeclarationStatement, VariableDeclarationHandler>();
        
        
        //Statements blocks
        parserBuilder.WithParserHandler<BlockStatement, BlockHandler>();
        parserBuilder.WithParserHandler<EmptyBlockStatement, EmptyBlockHandler>();
        parserBuilder.WithParserHandler<BreakStatement, BreakHandler>();
        parserBuilder.WithParserHandler<ForLoopStatement, ForBlockHandler>();
        parserBuilder.WithParserHandler<IfBlockStatement, IfBlockHandler>();
        parserBuilder.WithParserHandler<WhileBlockStatement, WhileBlockHandler>();
        
        
        //Expressions
        parserBuilder.WithParserHandler<Expression, ExpressionHandler>();
     
        
        //Expressions literals
        parserBuilder.WithParserHandler<IdentifierLiteral, IdetifierHandler>();
        parserBuilder.WithParserHandler<FunctionCallExpression, FunctionCallHandler>();
        parserBuilder.WithParserHandler<Literal, LiteralHandler>();
        
        return parserBuilder.Build();
    }
}