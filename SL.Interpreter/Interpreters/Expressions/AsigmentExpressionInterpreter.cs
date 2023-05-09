using SL.Parser.Parsing.AST.Expressions;

namespace SL.Interpreter.Interpreters.Expressions;

public class AsigmentExpressionInterpreter : IInterpreter<AssigmentExpression>
{
    public async Task<object> Interpreter(AssigmentExpression node, ProgramContext program, InterpreterFactory factory)
    {
        var left = await factory.InterpreterNode(node.Left);
        var right = await factory.InterpreterNode(node.Right);
        var operation = node.Operator;

        if (!IsProgramVariable(left))
        {
            throw new  Exception($"Assignet Object need to be Variable but is {left} ");
        }

        var variable = left as ProgramVariable;

        switch (operation)
        {
            case "=":
                HandleAssigment(variable, right, program);
                break;
            case "+=":
                HandleNumbericAssigment(variable, right, (a, b) => a + b);
                break;
            case "-=":
                HandleNumbericAssigment(variable, right, (a, b) => a - b);
                break;
            case "*=":
                HandleNumbericAssigment(variable, right, (a, b) => a * b);
                break;
            case "/=":
                HandleNumbericAssigment(variable, right, (a, b) => a / b);
                break;
            case "^=":
                HandleNumbericAssigment(variable, right, (a, b) => (float)Math.Pow(a, b));
                break;
        }
        return left;
    }


    private void HandleAssigment(ProgramVariable left, object right, ProgramContext programContext)
    {
        if (!programContext.IsValueMatchType(left.type, right))
        {
            throw  new Exception($"Type not match ");
        }

        if (right is ProgramVariable programVariable)
        {
            HandleAssigment(left, programVariable.value, programContext);
            return;
        }

        left.value = right;
    }

    private void HandleNumbericAssigment(ProgramVariable variable, object value, Func<float, float, float> action)
    {
        if (!IsNumeric(variable.value))
        {
            throw new Exception($"Assigment action require numberic variable instead of {variable.type}");
        }
        if (!IsNumeric(value))
        {
            throw new Exception($"Assigned Value need to be type of numeric {value}");
        }
        
        var currentValue = (float)variable.value;
        var newValue = (float)value;

        variable.value = action.Invoke(currentValue, newValue);
    }


    private bool IsProgramVariable(object left)
    {
        return left is ProgramVariable;
    }

    private bool IsNumeric(object variable)
    {
        return variable is float;
    }

 
}