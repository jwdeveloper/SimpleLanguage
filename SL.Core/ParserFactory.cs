using SL.Core.Parsing;

namespace SL.Core;

public class ParserFactory
{
    public  static Parser CreateParser()
    {
        var parserBuilder = new ParserBuilder();
        return parserBuilder.Build();
    }
}