namespace SL.Core.Parsing.AST;

public class Block : Statement
{
   private readonly List<Statement> _statements;
   private readonly string _name;

   public Block(List<Statement> statements, string name)
   {
      _statements = statements;
      _name = name == string.Empty ? GetType().Name : name;
   }

   public override void Invoke()
   {
      foreach (var statement in _statements)
      {
         statement.Invoke();
      }
   }

   public override IEnumerable<Node> Children()
   {
      return _statements;
   }

   public override string Name()
   {
      return _name;
   }
}