using SL.Parser.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public class ClassStatementInterpreter : IInterpreter<ClassDeclarationStatement>
{
    public Task<object> Interpreter(ClassDeclarationStatement node, ProgramContext program, InterpreterFactory factory)
    {
        var functionModel = new ProgramClass();
        functionModel.Name = node.ClassNameIdentifier.IdentifierName;


        return null;
        //return program.CreateFunction(functionModel);
    }
}