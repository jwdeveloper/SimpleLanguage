using System.Text;
using SL.Tokenizer.Interfaces;
using SL.Tokenizer.Models;

namespace SL.Tokenizer.Handlers;

public class NumberHandler : ITokenHandler
{

 
    public async Task<Token> Handle(string symbol, IIterator<char> iterator, CancellationToken ctx)
    {
        var sb = new StringBuilder();
        sb.Append(symbol);

        while (!iterator.IsEnded())
        {
            if (ctx.IsCancellationRequested)
            {
                return Token.Canceled(iterator.Position());
            }

            var next = iterator.Peek(1);
            if (!char.IsNumber(next) && next != '.')
            {
                break;
            }

            var current = iterator.Advance();
            sb.Append(current);
        }

        var number = sb.ToString();
        if (number == ".")
        {
            return new Token(TokenType.DOT, number, iterator.Position());
        }
        
        if (!ValidateNumber(number, out var error))
        {
            return Token.BadToken($"InvalidNumber {error}: {number}", iterator.Position());
        }

        return new Token(TokenType.NUMBER, number, iterator.Position());
    }

    public bool ValidateNumber(string number, out string message)
    {
        message = string.Empty;

        var chars = number.ToCharArray().ToList();
        var dots = chars.FindAll(c => c.Equals('.'));
        if (dots.Count == 0)
        {
            return true;
        }

        if (dots.Count > 1)
        {
            message = "There should be only one dot in number";
            return false;
        }

        if (chars[^1] == '.')
        {
            message = "Dot can't be place after digits";
            return false;
        }

        return true;
    }
}