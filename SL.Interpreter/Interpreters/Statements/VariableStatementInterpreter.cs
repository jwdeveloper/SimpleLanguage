using SL.Parser.Parsing.AST;

namespace SL.Interpreter.Interpreters;

public class VariableStatementInterpreter : IInterpreter<VariableStatement>
{
    public async Task<object> Interpreter(VariableStatement node, ProgramContext program, InterpreterFactory factory)
    {
        var variableTypeName = node.VariableType;
        if (program.IsTypeExists(variableTypeName))
        {
            throw new Exception($"RegistrationType does not exists {variableTypeName}");
        }


        var declarations = node.VariableDeclarations;
        
        object assigmentValue = null;
        var varaibles = new List<ProgramVariable>();
        foreach(var declaration in declarations)
        {
            if (program.IsVariableExists(declaration.GetVariableDeclarationName))
            {
                throw new Exception($"Variable already exists {declaration.GetVariableDeclarationName}");
            }

            if (declaration.HasAssigmentExpression)
            {
                assigmentValue = await factory.InterpreterNode(declaration.AssigmentExpression);
            }
            
            varaibles.Add(new ProgramVariable
            {
                name = declaration.GetVariableDeclarationName,
                type = variableTypeName,
                value = null
            });
        }

        if (assigmentValue is null)
        {
            assigmentValue = GetDefaultValue(variableTypeName);
        }

        if (assigmentValue is ProgramVariable programVariable)
        {
            assigmentValue = GetProgramVariableValue(programVariable);
        }
        

        if (!program.IsValueMatchType(variableTypeName, assigmentValue))
        {
            throw new Exception($"AssigmentValue not match type: {variableTypeName}   value: {assigmentValue}");
        }

        foreach(var variable in varaibles)
        {
            variable.value = assigmentValue;
            program.CreateVariable(variable);
        }
        return varaibles;
    }


    private object GetDefaultValue(string objectType)
    {
        if (objectType == "number")
        {
            return 0;
        }
        if (objectType == "text")
        {
            return string.Empty;
        }
        if (objectType == "bool")
        {
            return false;
        }
        if (objectType == "var")
        {
            return null;
        }
        throw new Exception($"Unknown Type {objectType}");
    }
    
    private object GetProgramVariableValue(ProgramVariable program)
    {
        if (program.value is ProgramVariable programVariable)
        {
            return GetProgramVariableValue(programVariable);
        }

        return program.value;
    }

   
    
}