using SimpleLangInterpreter.Exceptions;
using SimpleLangInterpreter.Node;

namespace SimpleLangInterpreter.Core;

public class Parser : TokenIterator
{

    public Parser(List<SyntaxToken> tokens) : base(tokens)
    {
        
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
                Advance();
                continue;
            }
            if (Current().Symbol == "(")
            {
                
                break;
            }
            if (Current().Symbol == "{")
            {
                
                break;
            }

            if (Current().TokenType == TokenType.WhiteSpace)
            {
                Advance();
                continue;
            }
            
            currentTokens.Add(Current());
            Advance();
        }
        return root;
    }


    public ExpresionSyntax ParseLineExpression(List<SyntaxToken> tokens)
    {

        var bestBinary = FindBestBinaryTokens(tokens);
        if (bestBinary.Key == -1)
        {
            return new NumberNode(tokens[0]);
        }
        var index = bestBinary.Key;
        var binaryThing = bestBinary.Value;
        
        var left = tokens.GetRange(0, index);
        var right = tokens.GetRange(index + 1, (tokens.Count - 1)-index );

        var leftExp = ParseLineExpression(left);
        var rightExp = ParseLineExpression(right);
        
        return new BinaryExpression(binaryThing,leftExp, rightExp);
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