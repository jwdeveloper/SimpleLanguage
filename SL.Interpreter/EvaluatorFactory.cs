using System.Text;
using SL.Parser.Parsing.AST;
using SL.Parser.Parsing.AST.Expressions;
using SL.Interpreter.Interpreters;
using SL.Interpreter.Interpreters.Expressions;
using SL.Parser.Parsing.AST.Statements;

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
            //Statements
            .WithInterpreter<Statement, StatementInterpreter>()
            .WithInterpreter<VariableStatement, VariableStatementInterpreter>()
            .WithInterpreter<IfStatement, IfBlockInterpreter>()
            .WithInterpreter<WhileStatement, WhileBlockInterpeter>()
            .WithInterpreter<ForStatement, ForBlockInterpreter>()
            .WithInterpreter<ForeachStatement,ForeachBlockInterpreter>()
            .WithInterpreter<FunctionDeclarationStatement, FunctionStatementInterpreter>()
            .WithInterpreter<BlockStatement,BlockInterpreter>()
            .WithInterpreter<EmptyStatement>((_, _, _) => Task.FromResult<object>(true))
            .WithInterpreter<ExpresionStatement, ExpresionStatementInterpreter>()
            .WithInterpreter<ReturnStatement, ReturnInterpreter>()
            
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
            .WithSystemFunction("range", "list", async (args, program) =>
            {
                if (args.Length == 1)
                {
                    var list = new List<object>();
                    var to = (float)args[0];
                    for (var i = 0; i <= to; i++)
                    {
                        list.Add(i);
                    }
                    return new ProgramVariable
                    {
                        type = "list",
                        value = list,
                        name = Guid.NewGuid().ToString()
                    };
                }
                if (args.Length == 1)
                {
                    var list = new List<object>();
                    var from = (float)args[0];
                    var to = (float) args[1];
                    for (var i = from; i <= to; i++)
                    {
                        list.Add(i);
                    }
                    return new ProgramVariable
                    {
                        type = "list",
                        value = list,
                        name = Guid.NewGuid().ToString()
                    };
                }
                throw new Exception("Bad number of arguments");
            })
            .WithSystemFunction("list", "list", async (args, program) =>
            {
                return new ProgramVariable
                {
                    type = "list",
                    value = new List<object>(),
                    name = Guid.NewGuid().ToString()
                };
            })
            .Build();
    }
}