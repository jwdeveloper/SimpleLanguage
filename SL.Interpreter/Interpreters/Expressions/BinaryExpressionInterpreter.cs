using SL.Parser.Parsing.AST.Expressions;

namespace SL.Interpreter.Interpreters.Expressions;

public class BinaryExpressionInterpreter : IInterpreter<BinaryExpression>
{
    public async Task<object> Interpreter(BinaryExpression node, ProgramContext program, InterpreterFactory factory)
    {
        var left = await factory.InterpreterNode(node.Left);
        var right = await factory.InterpreterNode(node.Right);

        switch (node.Operator)
        {
            case "==":
            case "is":
                return HandleEquality(left, right);
            case "!=":
                return !HandleEquality(left, right);
            case "+":
                if (IsString(left))
                {
                    return HandleStringOperation(left, right, (a, b) => a + b);
                }
                return HandleNumericOperation(left, right, (a, b) => a + b);
            case "-":
                return HandleNumericOperation(left, right, (a, b) => a - b);
            case "*":
                return HandleNumericOperation(left, right, (a, b) => a * b);
            case "/":
                return HandleNumericOperation(left, right, (a, b) => a / b);
            case "^":
                return HandleNumericOperation(left, right, (a, b) => (float)Math.Pow(a, b));
            case ">":
                return HandleNumericOperation(left, right, (a, b) => a > b);
            case ">=":
                return HandleNumericOperation(left, right, (a, b) => a >= b);
            case "<":
                return HandleNumericOperation(left, right, (a, b) => a < b);
            case "<=":
                return HandleNumericOperation(left, right, (a, b) => a <= b);
            case "&&":
            case "and":
                return HandleLogicalOperation(left, right, program, (a, b) => a && b);
            case "||":
            case "or":
                return HandleLogicalOperation(left, right, program, (a, b) => a || b);
            default:
                throw new Exception($"Unknown expression operation: {node.Operator}");
        }
    }

    private bool HandleEquality(object left, object right)
    {
        var leftValue = GetValue(left);
        var rightValue = GetValue(right);
        return leftValue.Equals(rightValue);
    }

    private object HandleNumericOperation(object left, object right, Func<float, float, object> action)
    {
        if (!IsNumeric(left) || !IsNumeric(right))
        {
            throw new Exception($"Numeric Operation require left and right Numeric values");
        }

        var leftValue = GetNumericValue(left);
        var rightValue = GetNumericValue(right);

        return action.Invoke(leftValue, rightValue);
    }

    
    private object HandleStringOperation(object left, object right, Func<string, string, object> action)
    {
        if (!IsString(left) || !IsString(right))
        {
            throw new Exception($"Numeric Operation require left and right String values");
        }

        var leftValue = GetStringValue(left);
        var rightValue = GetStringValue(right);

        return action.Invoke(leftValue, rightValue);
    }

    private bool HandleLogicalOperation(object left, object right, ProgramContext programContext,
        Func<bool, bool, bool> action)
    {
        if (!programContext.IsBoolean(left) || !programContext.IsBoolean(right))
        {
            throw new Exception($"Numeric Operation require left and right Boolean values");
        }

        var leftValue = programContext.GetBoolValue(left);
        var rightValue = programContext.GetBoolValue(right);

        return action.Invoke(leftValue, rightValue);
    }


    private bool IsString(object target)
    {
        if (target is string)
        {
            return true;
        }

        if (target is ProgramVariable variable)
        {
            return IsString(variable.value);
        }

        return false;
    }
    
    private bool IsNumeric(object target)
    {
        if (target is float)
        {
            return true;
        }

        if (target is ProgramVariable variable)
        {
            return IsNumeric(variable.value);
        }

        return false;
    }

    private float GetNumericValue(object target)
    {
        if (target is float floatValue)
        {
            return floatValue;
        }

        if (target is ProgramVariable variable)
        {
            return GetNumericValue(variable.value);
        }

        throw new Exception("Target object is not of numeric type");
    }
    
    private string GetStringValue(object target)
    {
        if (target is string floatValue)
        {
            return floatValue;
        }

        if (target is ProgramVariable variable)
        {
            return GetStringValue(variable.value);
        }

        throw new Exception("Target object is not of string type");
    }


    private object GetValue(object target)
    {
        if (target is ProgramVariable variable)
        {
            return GetValue(variable.value);
        }

        return target;
    }
}