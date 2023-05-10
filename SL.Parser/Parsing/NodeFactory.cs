using System.Globalization;
using System.Reflection;
using SL.Parser.Api;
using SL.Parser.Common;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;

namespace SL.Parser.Parsing;

public class NodeFactory
{
    private readonly CancellationToken _ctx;
    private readonly Dictionary<Type, object> _parsers;
    private readonly ITokenIterator _tokenIterator;
    private MethodInfo _methodInfo;
    private PropertyInfo _propertyInfo;


    public NodeFactory(Dictionary<Type, object> parsers, ITokenIterator tokenIterator, CancellationToken ctx)
    {
        _ctx = ctx;
        _parsers = parsers;
        _tokenIterator = tokenIterator;
    }

    
    public async Task<T> CreateNode<T>(params object[] parameters)
    {
        if (_ctx.IsCancellationRequested)
        {
            throw new Exception("Cancellation requested");
        }
        
        var parserType = typeof(T);
        if (!_parsers.ContainsKey(parserType))
        {
            throw new Exception($"Parser for type {parserType} not registered");
        }
        object interpreter = _parsers[parserType];
        _methodInfo = interpreter.GetType().GetMethod("Parse");
        
        var task = (Task)_methodInfo.Invoke(interpreter, new object[] { _tokenIterator, this, parameters});
        await task.ConfigureAwait(false);
        _propertyInfo =  task.GetType().GetProperty("Result");
        return (T)_propertyInfo.GetValue(task);
    }
    
}