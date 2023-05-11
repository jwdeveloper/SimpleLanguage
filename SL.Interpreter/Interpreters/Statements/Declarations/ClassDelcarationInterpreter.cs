using SL.Parser.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public class ClassDelcarationInterpreter : IInterpreter<ClassDeclarationStatement>
{
    public async Task<object> Interpreter(ClassDeclarationStatement node, ProgramContext program, InterpreterFactory factory)
    {
        var className = node.ClassNameIdentifier.IdentifierName;
        var functions = await InterpreteFunctions(node.ClassFunctions, factory);
        var constructors = await InterpreteFunctions(node.ClassConsturctors, factory);
        var fields = await InterpreteFields(node.ClassFields, factory);
      
        
        var programClass = new ProgramClass()
        {
          
        };


        program.CreateProgramClass(programClass);
        

        return programClass;
    }


    private async Task<List<ProgramFunction>> InterpreteFunctions(List<FunctionDeclarationStatement> functionDeclarationStatements, InterpreterFactory factory)
    {
        var result = new List<ProgramFunction>();
        foreach(var declaratio in functionDeclarationStatements)
        {
            var programFunction = await factory.InterpreterNode(declaratio);
            result.Add((ProgramFunction)programFunction);
        }
        return result;
    }
    
    
    private async Task<List<ProgramVariable>> InterpreteFields(List<VariableStatement> functionDeclarationStatements, InterpreterFactory factory)
    {
        var result = new List<ProgramVariable>();
        foreach(var declaratio in functionDeclarationStatements)
        {
            var programFunction = await factory.InterpreterNode(declaratio);
            var variables = (List<ProgramVariable>)programFunction;
            result.AddRange(variables);
        }
        return result;
    }
}