namespace SimpleLangInterpreter.Core;

public class DefaultOperations
{
    public string Name { get; set; }
    public string Value { get; set; }
    
    public static List<DefaultOperations> GetDefaultOperations()
    {
        return new List<DefaultOperations>()
        {
            FOR,
            WHILE,
            IF,
        };
    }
    
    
    public static DefaultOperations FOR = new()
    {
        Name = "FOR",
        Value = "for"
    };
    
       
    public static DefaultOperations WHILE = new()
    {
        Name = "WHILE",
        Value = "while"
    };
    
       
    public static DefaultOperations IF = new()
    {
        Name = "IF",
        Value = "if"
    };
}