using SL.Parser.Parsing;

namespace SL.Parser;

public class ParserFactory
{
    public  static Parsing.Parser CreateParser()
    {
        var parserBuilder = new ParserBuilder();
        return parserBuilder.Build();
    }
}