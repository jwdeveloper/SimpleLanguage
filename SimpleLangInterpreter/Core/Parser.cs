using SimpleLangInterpreter.Exceptions;
using SimpleLangInterpreter.Node;

namespace SimpleLangInterpreter.Core;

public class Parser : TokenIterator
{

    public Parser(List<SyntaxToken> tokens) : base(tokens)
    {
        this.tokens.RemoveAll(e => e.TokenType == TokenType.WhiteSpace);


    }
    
    public Program Parse()
    {
        Reset();
        Advance();
        var root = new Program();
        var currentTokens = new List<SyntaxToken>();
        while (IsValid())
        {
            if (Current().Symbol == ";")
            {
                var result = ParseLineExpression(currentTokens);
                root.Nodes.Add(result);
                currentTokens.Clear();
                Advance(); 
                continue;
            }
            if (Current().TokenType == TokenType.OperationToken)
            {
                switch (Current().Symbol)
                {
                    case "while":
                        root.Nodes.Add(ParseWhile());
                        break;
                    case "if":
                        root.Nodes.Add(ParseIf());
                        break;
                    case "for":
                        break;
                }
                continue;
            }

            if (Current().TokenType is TokenType.WhiteSpace or TokenType.Comment)
            {
                Advance();
                continue;
            }
            
            currentTokens.Add(Current());
            Advance();
        }
        return root;
    }


  
    public ExpresionSyntax ParseWhile()
    {
        var ifToken = MatchToken("while");
        var logicArgument = ParseLogicalArgument();
        var body = ParseBody();
        return new WhileExpression(ifToken, logicArgument, body);
    }

    public ExpresionSyntax ParseIf()
    {
        var ifToken = MatchToken("if");
        var logicArgument = ParseLogicalArgument();
        var body = ParseBody();

        if (Current().Symbol != "else")
        {
            return new IfExpression(ifToken, logicArgument, body, null);
        }
        Advance();
        var elseBody = ParseBody();
        return new IfExpression(ifToken, logicArgument, body, elseBody);
    }
    
    


    public ExpresionSyntax ParseLogicalArgument()
    {
        var openParentas = MatchToken("(");
        var content = getBetween("(", ")");
        var expresion = ParseLineExpression(content);
        var closeParentasis = MatchToken(")");
        return expresion;
    }
    
    public ExpresionSyntax ParseBody()
    {
        var openParentas = MatchToken("{");
        
        var bettween = getBetween("{", "}");

        var perser = new Parser(bettween);

        var expresion = perser.Parse();
        //var expresion = ParseLineExpression(bettween);
        var closeParentasis = MatchToken("}");
        return expresion;
    }


    public List<SyntaxToken> getBetween(string from, string to)
    {
        var openCount = 1;
        var closeCount = 0;
        var content = new List<SyntaxToken>();
        do
        {
            if (!IsValid())
            {
                break;
            }
            if (Current().Symbol == from)
            {
                openCount++;
            }

            if (Current().Symbol == to)
            {
                closeCount++;
            }
            content.Add(Current());
            if (openCount == closeCount)
            {
                break;
            }
            Advance();
        } while (openCount != closeCount);

        var last = content.Last();

        content.Remove(last);
        return content;
    }
    
    public List<SyntaxToken> getBetween(string from, string to, TokenIterator iterator)
    {
        var openCount = 1;
        var closeCount = 0;
        var content = new List<SyntaxToken>();
        do
        {
            if (!iterator.IsValid())
            {
                break;
            }
            if (iterator.Current().Symbol == from)
            {
                openCount++;
            }

            if (iterator.Current().Symbol == to)
            {
                closeCount++;
            }
            content.Add(iterator.Current());
            if (openCount == closeCount)
            {
                break;
            }
            iterator.Advance();
        } while (openCount != closeCount);

        var last = content.Last();

        content.Remove(last);
        return content;
    }


    public ExpresionSyntax ParseLineExpression(List<SyntaxToken> tokens)
    {
        if (tokens.Count == 0)
        {
            return new  UndefindExpression("No more tokens to parse");
        }

        
        var bestBinary = FindBestBinaryTokens(tokens);
        if (bestBinary.Key == -1)
        {
            if (tokens.Count == 2)
            {
                var contains = tokens.Find(c => c.Symbol == "(" || c.Symbol == ")");
                if (contains != null)
                {
                    tokens.Remove(contains);
                }
            }
            
            if (tokens.Count == 1)
            {
                var token = tokens[0];
                switch (token.TokenType)
                {
                    case TokenType.NumberToken:
                        return new NumberNode(token);
                    case TokenType.StringToken:
                        return new StringNode(token);
                    case TokenType.KeywordToken:
                        if (token.Symbol == "true" || token.Symbol == "false")
                        {
                            return new BoolNode(token);
                        }
                        return new UndefindExpression("Unknown type of token");
                    
                    case TokenType.LitteralToken:
                        return new VariableNode(token);
                    default:
                        return new UndefindExpression("Unknown type of token");
                } 
            }
            
            if (isFunctionCall(tokens, out var expresion))
            {
                return expresion;
            }

            if (isCreateVariable(tokens, out var create))
            {
                return create;
            }
        }
        var index = bestBinary.Key;
        var binaryThing = bestBinary.Value;
        
        var left = tokens.GetRange(0, index);
        var right = tokens.GetRange(index + 1, (tokens.Count - 1)-index );

        var leftExp = ParseLineExpression(left);
        var rightExp = ParseLineExpression(right);
        
        return new BinaryExpression(binaryThing,leftExp, rightExp);
    }


    public bool isCreateVariable(List<SyntaxToken> tokens, out ExpresionSyntax expresionSyntax)
    {
        expresionSyntax = null;
        if (tokens.Count != 2)
        {
            return false;
        }
        expresionSyntax = new CreateVariableExpersion(tokens[0],tokens[1]);
        return true;
    }
    
    
    public bool isFunctionCall(List<SyntaxToken> tokens, out ExpresionSyntax expresionSyntax)
    {
        expresionSyntax = null;
        var iterator = new TokenIterator(tokens);
        iterator.Advance();

        var functionName = iterator.Current();
        iterator.Advance();
        var next = iterator.Current();
        iterator.Advance();
        if (functionName.TokenType != TokenType.LitteralToken && next.Symbol != "(")
        {
            return false;
        }
        var content = getBetween("(", ")", iterator);
        content.RemoveAll(e => e.Symbol == ",");

        var arguments = new List<ExpresionSyntax>();
        foreach(var con in content)
        {
          var exp =  ParseLineExpression(new List<SyntaxToken>() {con});
          arguments.Add(exp);
        }

        expresionSyntax = new FunctionCallExpression(functionName, functionName.Symbol, arguments);
        return true;
    }



    
    public KeyValuePair<int,SyntaxToken> FindBestBinaryTokens(List<SyntaxToken> tokens)
    {
        SyntaxToken token = null;
        var biggerValue = int.MaxValue;
        var index = -1;
        for (var i = 0; i < tokens.Count; i++)
        {
            var current = tokens[i];
            if (current.TokenType != TokenType.BinaryToken)
            {
                continue;
            }
            var number = GetBinaryTokenValue(current.Symbol);
            if (number <= biggerValue)
            {
                biggerValue = number;
                token = current;
                index = i;
            }
        }
        return new KeyValuePair<int, SyntaxToken>(index, token);
    }
    
    
    public List<Operation> Interpet(List<SyntaxToken> tokens)
    {
        var operations = new List<Operation>();
        for (var current = 0; current < tokens.Count; current++)
        {
            var token = tokens[current];
            if (token.Name == "OPERATION")
            {
                var openArguments = current+1;
                if(!ContainsTokenAtPosition(tokens, openArguments, SyntaxToken.OPEN_FUNCTION))
                {
                    throw new LangFormatException($"Bad format of {token.Name} not open '('");
                }
                var closeArgumetns = FindClosingTokenIndex(tokens, openArguments, SyntaxToken.OPEN_FUNCTION, SyntaxToken.CLOSE_FUNCTION);
                if(closeArgumetns == -1)
                {
                    throw new LangFormatException($"Bad format of {token.Name} not close ')'");
                }
                var argumentTokens = CloneSymbols(tokens, openArguments+1, closeArgumetns);
                
                
                var openContent = closeArgumetns + 1;
                if(!ContainsTokenAtPosition(tokens, openContent, SyntaxToken.OPEN_OPERATION))
                {
                    throw new LangFormatException($"Bad format of {token.Name} not open content");
                }
                var closeContent = FindClosingTokenIndex(tokens, openContent, SyntaxToken.OPEN_OPERATION, SyntaxToken.CLOSE_OPERATION);
                if(closeContent == -1)
                {
                    throw new LangFormatException($"Bad format of {token.Name} not close content");
                }

                var contentTokens = CloneSymbols(tokens, openContent, closeContent);
                var localOperations = Interpet(contentTokens);

                var operation = new Operation()
                {
                    Arguments = argumentTokens,
                    Operations = localOperations,
                    Name = token.Name
                };
                
                operations.Add(operation);
                current = closeContent;
                continue;
            } 
        }
        return operations;
    }
    
    private SyntaxToken MatchToken(string content, bool current = true)
    {
        if (current)
        {
            var peek = Current();
            if (peek.Symbol ==  content)
                return Advance(); 
        }
        else
        {
            var peek = Peek(1);
            if (peek.Symbol ==  content)
                return Advance();
        }
        
        
      

        throw new Exception("Token not match! " + content);
        return new SyntaxToken
        {
            Name = "Error",
            TokenType = TokenType.Undefined,
            Symbol = "Error"
            
        };
    }
    
    private SyntaxToken MatchToken(TokenType type)
    {
        if (Current().TokenType ==  type)
            return Advance();

        throw new Exception("Token not match! " + type);
        return new SyntaxToken
        {
            Name = "Error",
            TokenType = type,
            Symbol = "Error"
        };
    }
    
    
    public List<SyntaxToken> CloneSymbols(List<SyntaxToken> symbols, int from, int to)
    {
        List<SyntaxToken> result = new List<SyntaxToken>();
        for (var i = from; i < to; i++)
        {
            result.Add(symbols[i]);
        }
        return result;
    }
    
    public bool ContainsTokenAtPosition(List<SyntaxToken> tokens, int current, SyntaxToken expectedSyntaxToken)
    {
        if (tokens.Count-1 <= current)
        {
            return false;
        }

        if (tokens[current] != expectedSyntaxToken)
        {
            return false;
        }
        
        return true;
    }
    
    public int FindClosingTokenIndex(List<SyntaxToken> tokens, int current, SyntaxToken expectedOpen, SyntaxToken exceptedClose)
    {
        var openCount = 0;
        var closeCount = 0;

        if (current > tokens.Count)
        {
            return -1;
        }
        
        for (var i = current; i < tokens.Count; i++)
        {
            var token = tokens[i];
            if (token == expectedOpen)
            {
                openCount++;
            }
            if (token == exceptedClose)
            {
                closeCount++;
            }
            if (openCount == closeCount)
            {
                return i;
            }
        }
        return -1;
    }

   
    public static int GetBinaryTokenValue(string kind)
    {
        switch (kind)
        {
            case "^":
                return 7;
            
            case "/":
                return 6;
            case "*":
                return 5;
            case "+":
            case "-":
                return 4;
            
            case "==":
                return 3;

            default:
                return 0;
        }
    }
}