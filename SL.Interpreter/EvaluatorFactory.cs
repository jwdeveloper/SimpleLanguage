using System.Text;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;
using SL.Interpreter.Interpreters;
using SL.Interpreter.Interpreters.Expressions;
using SL.Parser.Parsing.AST.Statements;

namespace SL.Interpreter;

public class EvaluatorFactory
{
    public static Evaluator CreateEvaluator(CancellationToken  ctx = new CancellationToken())
    {
        return new EvaluatorBuilder(ctx)
            .WithInterpreter<SlProgram>(async (program, context, factory) =>
            {
                foreach (var statement in program.Statements)
                {
                    await factory.InterpreterNode(statement);
                }
                return true;
            })
            //Statements
            .WithInterpreter<Statement, StatementInterpreter>()
            .WithInterpreter<VariableStatement, VariableStatementInterpreter>()
            .WithInterpreter<IfBlockStatement, IfBlockInterpreter>()
            .WithInterpreter<WhileBlockStatement, WhileBlockInterpeter>()
            .WithInterpreter<ForStatement, ForBlockInterpreter>()
            .WithInterpreter<ForeachStatement,ForeachBlockInterpreter>()
            .WithInterpreter<FunctionDeclarationStatement, FunctionStatementInterpreter>()
            .WithInterpreter<BlockStatement,BlockInterpreter>()
            .WithInterpreter<EmptyBlockStatement>((_, _, _) => Task.FromResult<object>(true))
            .WithInterpreter<ExpresionStatement, ExpresionStatementInterpreter>()
            .WithInterpreter<ReturnStatement, ReturnInterpreter>()
            .WithInterpreter<BreakStatement,BreakInterpreter>()
            
            //Expressions
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
                    var value = program.GetVariableValue(args[i], "var");
                    var msg = value == null ? "NULL" : value;

                    messages.Append(msg);
                    if (i != args.Length - 1)
                    {
                        messages.Append(' ');
                    }
                }

                program.AddConsoleOutput(messages.ToString());
                return program.IsCancelRequested;
            })
            .WithSystemFunction("clear", "var", async (args, program) =>
            {
                program.ClearConsole();
                return program.IsCancelRequested;
            })
            .WithSystemFunction("sleep", "var", async (args, program) =>
            {
                if (args.Length != 1)
                {
                    throw new Exception("Bad number of arguments");
                }
                var seconds = (float)program.GetVariableValue(args[0], "number");
                await Task.Delay((int)seconds, program.CANCELLATION_TOKEN);
                return program.IsCancelRequested;
            })
            .WithSystemFunction("range", "list", async (args, program) =>
            {
                if (args.Length == 1)
                {
                    var list = new List<object>();
                    var value = (float)program.GetVariableValue(args[0], "number");
                    if (value < 0)
                    {
                        value = 0;
                    }
                    for (float i = 0; i < value; i++)
                    {
                        list.Add(i);
                    }
                    return new ProgramVariable
                    {
                        Type = "list",
                        Value = list,
                        Name = Guid.NewGuid().ToString()
                    };
                }
                if (args.Length == 2)
                {
                    var list = new List<object>();
                    var from = (float)program.GetVariableValue(args[0], "number");
                    var to =  (float)program.GetVariableValue(args[1], "number");
                    for (float i = from; i <= to; i++)
                    {
                        list.Add(i);
                    }
                    return new ProgramVariable
                    {
                        Type = "list",
                        Value = list,
                        Name = Guid.NewGuid().ToString()
                    };
                }
                throw new Exception("Bad number of arguments");
            })
            .WithSystemFunction("list", "list", async (args, program) =>
            {
                return new ProgramVariable
                {
                    Type = "list",
                    Value = new List<object>(),
                    Name = Guid.NewGuid().ToString()
                };
            })
            .Build();
    }
}