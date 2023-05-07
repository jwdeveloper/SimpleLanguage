using SL.Core.Lexing;


namespace SL.Core.Parsing;

public class ParserBuilder
{

    public Parser Build()
    {
        return new Parser(new NodeFactory());
    }
}