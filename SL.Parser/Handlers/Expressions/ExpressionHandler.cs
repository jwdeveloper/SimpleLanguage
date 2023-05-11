using SL.Parser.Api;
using SL.Parser.Parsing.AST;


namespace SL.Parser.Parsing.Handlers.Expressions;

public class ExpressionHandler : IParserHandler<Expression>
{
    public async Task<Expression> Parse(ITokenIterator tokenIterator, NodeFactory parserFactory, object[] parameters)
    {
        var expressionLookup = ExpressionLookup.Assigment;

        if (parameters.Length == 1)
        {
            expressionLookup = (ExpressionLookup)parameters[0];
        }


        switch (expressionLookup)
        {
            case ExpressionLookup.Assigment:
                var assigmentHandler = new ExpressionAssigmentHandler();
                return await assigmentHandler.Parse(tokenIterator, parserFactory, parameters);   
            case ExpressionLookup.Binary:
                var binaryHandler = new ExpressionBinaryHandler();
                return await binaryHandler.Parse(tokenIterator, parserFactory, parameters);   
        }

        throw new Exception("Unknow expression type");
    }

    public enum ExpressionLookup
    {
        Assigment,
        Binary
    }

  
}