using SL.Parser.Common;
using SL.Parser.Lexing;
using SL.Parser.Lexing.Handlers;

namespace SL.Parser;

public class TokenizerFactory
{
    public static Tokenizer CreateLexer(string content)
    {
        var builder = new LexerBuilder();
        builder.WithContent(content);
        builder.WithIgnore(" ");
        builder.WithIgnore("\n");
        builder.WithIgnore("\r");

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
        builder.WithSymbol(new[] { "*", "/", "^", "%","mod" }, TokenType.BINARY_MULTIPLICATION_OPERATOR);
        builder.WithSymbol(new[] { "+=", "-=", "*=", "/=", "^=" }, TokenType.COMPLEX_ASSIGMENT);


        builder.WithSymbol(new[] { "var", "number", "text", "bool" }, TokenType.OBJECT_TYPE);
        builder.WithSymbol(new[] { "&&", "and", "||", "or", "!" }, TokenType.LOGICAL_OPERATOR);
        builder.WithSymbol(new[] { "==", "is", "!=", ">", ">=", "<", "<=" }, TokenType.EQUALITY_OPREATOR);
        builder.WithSymbol(new[] { "if", "else", "while", "do", "for", "in", "null", "function", "return","break","class","new","this"},
            TokenType.KEYWORLD);
        builder.WithSymbol(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "." }, new NumberHandler());
        return builder.Build();
    }
}