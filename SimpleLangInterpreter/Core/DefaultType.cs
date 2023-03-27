namespace SimpleLangInterpreter.Core;

public class DefaultType
{
    public string Name { get; set; }
    public string Value { get; set; }

    public static List<DefaultType> GetDefaultTypes()
    {
        return new List<DefaultType>()
        {
            Number(),
            WhatEver(),
            Text()
        };
    }

    public static DefaultType Number()
    {
        return new DefaultType()
        {
            Name = "Number",
            Value = "number"
        };
    }

    public static DefaultType WhatEver()
    {
        return new DefaultType()
        {
            Name = "WhatEver",
            Value = "var"
        };
    }
    
    public static DefaultType Text()
    {
        return new DefaultType()
        {
            Name = "Text",
            Value = "text"
        };
    }
    
}