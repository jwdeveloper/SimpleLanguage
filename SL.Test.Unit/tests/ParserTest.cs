using System.Text.Json;
using System.Text.Json.Nodes;
using SL.Core.Parsing.AST;

namespace SL.Test.Unit;

public class ParserTest : ParserTestBase
{
    [Test]
    public void TestParser()
    {
        var content = @"
  
          function number Print(text value)
          {
            return;
          } 
        
          function number GetContent(number output)
          {
            return output + 1;
          } 
          
          var a = 1.23;
          var b = ""name"";
          var c = false;
          var d = a;
         
          if(a == b)
          {
            Print(""A is equal to B"");
          }
          else if(c == false)
          {
            Print(""C is false"");
          }

          for(var i = 0;i<10; i+= 1)
          {
            Print(i);
          }
  
          var x = 10;
          while(x > 10)
          {
            Print(""x is greater then 10"");
          }
";
        Assert.DoesNotThrowAsync(async () =>
        {
            var program = await CreateProgram(content);
            var model = program.ToJson();
            NodeAssert.Assert<Program>(program);
        });
    }
   
}