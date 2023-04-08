using SL.Core.Common;
using SL.Core.Lexing;
using SL.Core.Parsing.AST;

namespace SL.Core.Parsing;

public class Parser
{
    //litteral()
    
    //expression * expression
    //
    
    //(1 + 2) * (3-5)
    
    
    
    //pattern:line -> expressions end_of_line
    //pattern:declaration -> literal literal
    //pattern:assigment -> literal [Plus,Minus..] expression;
    //pattern:increase literal plus plus;
    
    //pattern:parameters -> open_parameters Notes[] close_Parameters
    //pattern:body ->  open_body Nodes[] close_body
    
    //pattern:function -> literal literal arguments body -> function
    
    //pattern:for -> keyword:for parameters body
    //pattern:if -> keyword:if parameters body 
    //pattern:if-else -> keyword:if parameters body else body
    //pattern:while keyword:while parameters body


    private readonly Lexer _lexer;
    private readonly Diagnostic _diagnostic;
    
    public Parser(Lexer lexer, Diagnostic diagnostic)
    {
        _lexer = lexer;
        _diagnostic = diagnostic;
    }

    public async Task<Block> Parse(CancellationToken ctx = new CancellationToken())
    {
        var tokens = await _lexer.LexAll(ctx);
        var last = tokens.Last();
        if (last.Type is TokenType.BAD_TOKEN)
        {
            _diagnostic.AddError();
            return new Block();
        }
        return new Program();
    }
}