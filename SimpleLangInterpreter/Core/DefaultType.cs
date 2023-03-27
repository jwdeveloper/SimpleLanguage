namespace SimpleLangInterpreter.Core;

public class DefaultType
{
    public string Name { get; set; }
    public string Value { get; set; }

    public static List<DefaultType> GetDefaultTypes()
    {
        return new List<DefaultType>()
        {
            
            WhatEver(),
            Text(),
            Bool(),
            Number(),
            Int(),
            Float(),
            Byte(),
            String()
        };
    }

 

    public static DefaultType WhatEver()
    {
        return new DefaultType()
        {
            Name = "Whatever",
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
    
    public static DefaultType Number()
    {
        return new DefaultType()
        {
            Name = "Number",
            Value = "number"
        };
    }
    
    public static DefaultType Int()
    {
        return new DefaultType()
        {
            Name = "Int",
            Value = "int"
        };
    }
    
    public static DefaultType Float()
    {
        return new DefaultType()
        {
            Name = "Float",
            Value = "float"
        };
    }
    
    public static DefaultType Bool()
    {
        return new DefaultType()
        {
            Name = "Bool",
            Value = "bool"
        };
    }
    
    public static DefaultType Byte()
    {
        return new DefaultType()
        {
            Name = "Byte",
            Value = "byte"
        };
    }
    public static DefaultType String()
    {
        return new DefaultType()
        {
            Name = "String",
            Value = "string"
        };
    }
}