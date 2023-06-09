using System.Text.Json;
using System.Text.Json.Serialization;
using SL.Parser.Models.Statements.Blocks;

namespace SL.Parser.Models;

public class SlProgram : BlockStatement
{
    public SlProgram(List<Statement> statements) : base(statements, "Program")
    {
    }

    public string ToJson()
    {
       return JsonSerializer.Serialize(GetModel(), new JsonSerializerOptions
       {
           WriteIndented = true,
           Converters = { new JsonStringEnumConverter() },
       });
    }
    
}