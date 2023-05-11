namespace SL.Parser.Parsing.AST;

public class BlockStatement : Statement
{
   public  List<Statement> Statements { get; }
   private readonly string _name;

   public BlockStatement(List<Statement> statements, string name = "block")
   {
      Statements = statements;
      _name = name == string.Empty ? GetType().Name : name;
   }

   
   
   public override IEnumerable<Node> Children()
   {
      return Statements;
   }
   public override string Name()
   {
      return _name;
   }
}