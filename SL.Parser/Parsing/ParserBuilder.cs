using SL.Parser.Lexing;


namespace SL.Parser.Parsing;

public class ParserBuilder
{

    public Parser Build()
    {
        return new Parser(new NodeFactory());
    }
}