using System.Text;
using SL.Core.Parsing.AST;
using SL.Core.Parsing.AST.Expressions;
using SL.Interpreter.Interpreters;
using SL.Interpreter.Interpreters.Expressions;

namespace SL.Interpreter;

public class EvaluatorFactory
{
    public static Evaluator CreateEvaluator()
    {
        return new EvaluatorBuilder()
            .WithInterpreter<SlProgram>(async (program, context, factory) =>
            {
                foreach (var statement in program.Statements)
                {
                    await factory.InterpreterNode(statement);
                }

                return true;
            })
            .WithInterpreter<Statement, StatementInterpreter>()
            .WithInterpreter<VariableStatement, VariableStatementInterpreter>()
            .WithInterpreter<IfStatement, IfBlockInterpreter>()
            .WithInterpreter<WhileStatement, WhileBlockInterpeter>()
            .WithInterpreter<ForStatement, ForBlockInterpreter>()
            .WithInterpreter<FunctionDeclarationStatement, FunctionStatementInterpreter>()
            .WithInterpreter<BlockStatement>(async (block, program, factory) =>
            {
                foreach (var statement in block.Statements)
                {
                    await factory.InterpreterNode(statement);
                }

                return true;
            })
            .WithInterpreter<EmptyStatement>((statement, program, factory) => Task.FromResult<object>(true))
            .WithInterpreter<ExpresionStatement>(async (statement, program, factory) =>
                await factory.InterpreterNode(statement.Expression))
            .WithInterpreter<FunctionCallExpression, FunctionCallExpressionInterpreter>()
            .WithInterpreter<BinaryExpression, BinaryExpressionInterpreter>()
            .WithInterpreter<AssigmentExpression, AsigmentExpressionInterpreter>()
            .WithInterpreter<NumericLiteral, LitteralExpressionInterpreter<NumericLiteral>>()
            .WithInterpreter<TextLiteral, LitteralExpressionInterpreter<TextLiteral>>()
            .WithInterpreter<BoolLiteral, LitteralExpressionInterpreter<BoolLiteral>>()
            .WithInterpreter<IdentifierLiteral, IdentifierExpressionInterpreter>()
            .WithSystemFunction("print", "var", async (args, program) =>
            {
                var messages = new StringBuilder();
                for (var i = 0; i < args.Length; i++)
                {
                    var arg = args[i];
                    if (arg is ProgramVariable variable)
                    {
                        arg = variable.value;
                    }

                    var msg = arg == null ? "NULL" : arg;

                    messages.Append(msg);
                    if (i != args.Length - 1)
                    {
                        messages.Append(' ');
                    }
                }

                program.AddConsoleOutput(messages.ToString());
                return true;
            })
            .WithSystemFunction("clear", "var", async (args, program) =>
            {
                program.ClearConsole();
                return true;
            })
            .WithSystemFunction("sleep", "var", async (args, program) =>
            {
                if (args.Length != 1)
                {
                    throw new Exception("Bad number of arguments");
                }

                var seconds = (float)args[0];
                
                Thread.Sleep((int)seconds);
                return true;
            })
            .Build();
    }
}