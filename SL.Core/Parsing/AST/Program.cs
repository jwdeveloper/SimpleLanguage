namespace SL.Core.Parsing.AST;

public class Program : BlockStatement
{
    public Program(List<Statement> statements) : base(statements, "Program")
    {
        
    }
}