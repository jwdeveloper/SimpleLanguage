using System.Dynamic;

namespace SL.Parser.Models.Expressions;

public class ClassInitializeExpressionStatement : Expression
{
    public FunctionCallExpression ClassInitializer { get; }
    public ClassInitializeExpressionStatement(FunctionCallExpression functionCallExpression) 
    {
        ClassInitializer = functionCallExpression;
    }

    public override dynamic GetModel()
    {
        dynamic model = new ExpandoObject();
        model.name = Name();
        model.classInitializer = ClassInitializer.GetModel();
        return model;
    }
}