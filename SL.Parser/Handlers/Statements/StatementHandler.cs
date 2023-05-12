using SL.Parser.Models;
using SL.Parser.Models.Statements;
using SL.Parser.Models.Statements.Blocks;
using SL.Parser.Models.Statements.Declarations;
using SL.Tokenizer.Exceptions;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Parser.Handlers.Statements;

public class StatementHandler : IParserHandler<Statement>
{
    public async Task<Statement> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        var token = tokenIterator.LookUp();
        switch (token.Type)
        {
            case TokenType.OPEN_BLOCK:
                return await parserFactory.CreateNode<BlockStatement>();
            case TokenType.END_OF_LINE:
                return await parserFactory.CreateNode<EmptyBlockStatement>();
            case TokenType.OBJECT_TYPE:
                return await parserFactory.CreateNode<VariableStatement>();
            case TokenType.KEYWORLD:
                if (token.Value == "if")
                    return await parserFactory.CreateNode<IfBlockStatement>();
                if (token.Value is "while" or "do")
                    return await parserFactory.CreateNode<WhileBlockStatement>();
                if (token.Value is "for")
                    return await parserFactory.CreateNode<ForLoopStatement>();
                if (token.Value is "return")
                    return await parserFactory.CreateNode<ReturnStatement>();
                if (token.Value is "break")
                    return await parserFactory.CreateNode<BreakStatement>();
                if (token.Value is "function")
                    return await parserFactory.CreateNode<FunctionDeclarationStatement>();
                if (token.Value is "class")
                    return await parserFactory.CreateNode<ClassDeclarationStatement>();
                if (token.Value is "new")
                    return await parserFactory.CreateNode<ExpresionStatement>();
                break;
            default:
                return await parserFactory.CreateNode<ExpresionStatement>();
        }

        throw new SyntaxException("Unexpected statement", token);
    }
}