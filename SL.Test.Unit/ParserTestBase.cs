using SL.Core.Common;
using SL.Core.Lexing;
using SL.Core.Lexing.Handlers;
using SL.Core.Parsing;
using SL.Core.Parsing.AST;

namespace SL.Test.Unit;

public class ParserTestBase
{
    public Parser CreateParser()
    {
        var parserBuilder = new ParserBuilder();
        return parserBuilder.Build();
    }


    
    protected async Task<Program> CreateProgram(string content)
    {
        var lexer = CreateLexer(content);
        var parserBuilder = new ParserBuilder();
        var iterator = await lexer.LexAllToInterator();
        return parserBuilder.Build().Parse(iterator);
    }

    protected  Lexer CreateLexer(string content)
    {
        var builder = new LexerBuilder();
        builder.WithContent(content);
        builder.WithIgnore(" ");
        builder.WithIgnore("\n");


        builder.WithSymbol("=", TokenType.ASSIGMENT);
 
        builder.WithSymbol(";", TokenType.END_OF_LINE);

        builder.WithSymbol("{", TokenType.OPEN_BLOCK);
        builder.WithSymbol("}", TokenType.CLOSE_BLOCK);

        builder.WithSymbol("(", TokenType.OPEN_ARGUMETNS);
        builder.WithSymbol(")", TokenType.CLOSE_ARGUMENTS);
        builder.WithSymbol(",", TokenType.COMMA);


        builder.WithSymbol("\"", new StringHandler());
        builder.WithSymbol(new[] { "true", "false" }, TokenType.BOOL);
        builder.WithSymbol(new[] { "+", "-" }, TokenType.BINARY_ADDATIVE_OPERATOR);
        builder.WithSymbol(new[] { "*", "/", "^" }, TokenType.BINARY_MULTIPLICATION_OPERATOR);
        builder.WithSymbol(new[] { "+=", "-=", "*=", "/=" ,"++","--" }, TokenType.COMPLEX_ASSIGMENT);
       
        
        builder.WithSymbol(new[] { "var", "number", "text", "bool" }, TokenType.OBJECT_TYPE);
        builder.WithSymbol(new[] { "&&", "and", "||", "or", "!" }, TokenType.LOGICAL_OPERATOR);
        builder.WithSymbol(new[] { "==", "is", "!=", "not", ">", ">=", "<", "<=" }, TokenType.EQUALITY_OPREATOR);
        builder.WithSymbol(new[] { "if", "else", "while", "do", "for", "in", "null" ,"function", "return"}, TokenType.KEYWORLD);
        builder.WithSymbol(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "." }, new NumberHandler());

        return builder.Build();
    }
}