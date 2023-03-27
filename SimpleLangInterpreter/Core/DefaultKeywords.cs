namespace SimpleLangInterpreter.Core;

public class DefaultKeywords
{
    public string Name { get; set; }
    public string Value { get; set; }
    
    

    public static List<DefaultKeywords> GetDefaultKeywords()
    {
        return new List<DefaultKeywords>()
        {
            IN,
            OF,
            IS,
            NOT,
            VOID,
            CLASS,
            TRUE,
            FALSE,
            RETURN,
            ELSE
           
        };
    }
      
    public static DefaultKeywords IN = new()
    {
        Name = "IN",
        Value = "in"
    };
    
    public static DefaultKeywords OF = new()
    {
        Name = "OF",
        Value = "of"
    };
    
    public static DefaultKeywords IS = new()
    {
        Name = "IS",
        Value = "is"
    };
    
    public static DefaultKeywords NOT = new()
    {
        Name = "NOT",
        Value = "not"
    };
    
    public static DefaultKeywords VOID = new()
    {
        Name = "VOID",
        Value = "void"
    };
    
    public static DefaultKeywords CLASS = new()
    {
        Name = "CLASS",
        Value = "class"
    };
    
    public static DefaultKeywords TRUE = new()
    {
        Name = "TRUE",
        Value = "true"
    };
    
    public static DefaultKeywords FALSE = new()
    {
        Name = "FALSE",
        Value = "false"
    };
    
    public static DefaultKeywords RETURN = new()
    {
        Name = "RETURN",
        Value = "return"
    };
    
    public static DefaultKeywords ELSE = new()
    {
        Name = "ELSE",
        Value = "else"
    };
}