using System.Dynamic;
using System.Text.Json;

namespace SL.Core.Parsing.AST;

public class Program : BlockStatement
{
    public Program(List<Statement> statements) : base(statements, "Program")
    {
        
    }

    public string ToJson()
    {
       return JsonSerializer.Serialize(GetModel(), new JsonSerializerOptions { WriteIndented = true });
    }
    
}