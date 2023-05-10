namespace SL.Interpreter.Interpreters;

public class ProgramClass
{
    
    public string Name { get; set; }

    public ProgramContext ProgramContext;
    
    public List<ProgramVariable> Fields{ get; set; }
    public List<ProgramFunction> Functions{ get; set; }

    public List<ProgramFunction> Consturctors{ get; set; }



    public ProgramClass NewInstance(params object[] objects)
    {
        var consturctor = Consturctors.First(e => e.Name == "consturctor#" + objects.Length);
        if (consturctor == null)
        {
            throw new Exception("Consturctor not found"); 
        }


        consturctor.Invoker.Invoke(objects, null);
    }
    
    
    public async Task<object> InvokeFunction(string name, params object[] objects)
    {
        var function = Functions.First(e => e.Name == name);
        if (function == null)
        {
            throw new Exception("Function not exists exception");
        }
       return await function.Invoker.Invoke(objects, ProgramContext);
    }
   
    
    public ProgramVariable GetField(string name)
    {
        var field = Fields.First(e => e.Name == name);
        if (field == null)
        {
            throw new Exception("Field not exists");
        }

        return field;
    }
    
    public void SetField(string name, object value)
    {
        var field = GetField(name);
        field.Value = value;
    }
}