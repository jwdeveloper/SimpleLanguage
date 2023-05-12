using SL.Parser.Handlers;
using SL.Parser.Handlers.Expressions;
using SL.Parser.Handlers.Expressions.Literals;
using SL.Parser.Handlers.Statements;
using SL.Parser.Handlers.Statements.Blocks;
using SL.Parser.Handlers.Statements.Declarations;
using SL.Parser.Handlers.Statements.Expressions;
using SL.Parser.Models;
using SL.Parser.Models.Expressions;
using SL.Parser.Models.Literals;
using SL.Parser.Models.Statements;
using SL.Parser.Models.Statements.Blocks;
using SL.Parser.Models.Statements.Declarations;
using SL.Tokenizer.Interfaces;

namespace SL.Parser;

public class ParserFactory
{
    public  static Parser CreateParser(ITokenIterator tokenIterator, CancellationToken ctx)
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
        parserBuilder.WithParserHandler<ClassDeclarationStatement, ClassDeclarationHandler>();
        parserBuilder.WithParserHandler<FunctionDeclarationStatement, FunctionDeclarationHandler>();
        parserBuilder.WithParserHandler<VariableDeclarationStatement, VariableDeclarationHandler>();
        parserBuilder.WithParserHandler<List<ParameterStatement>, ParameterStatementListHandler>();
        
        
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
        parserBuilder.WithParserHandler<ClassInitializeExpressionStatement, ClassInitializeHandler>();
        parserBuilder.WithParserHandler<Literal, LiteralHandler>();
        
        return parserBuilder.Build();
    }
}