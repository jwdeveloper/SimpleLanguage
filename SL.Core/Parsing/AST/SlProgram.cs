using System.Dynamic;
using System.Text.Json;

namespace SL.Core.Parsing.AST;

public class SlProgram : BlockStatement
{
    public SlProgram(List<Statement> statements) : base(statements, "Program")
    {
        
    }

    public string ToJson()
    {
       return JsonSerializer.Serialize(GetModel(), new JsonSerializerOptions { WriteIndented = true });
    }
    
}