using SL.Core.Api;
using SL.Core.Common;

namespace SL.Core.Lexing;

public class CharIterator : IIterator<char>
{
    private readonly char[] _target;
    private char _current;
    private char _endValue;
    private Position _position;


    public CharIterator(char[] target, Position position)
    {
        _target = target;
        _position = position;
        _endValue = '\0';
        _current = _endValue;
    }


    public char Current()
    {
        return Peek(0);
    }

    public char Advance()
    {
        var value = Peek(1);
        if (value == _endValue)
        {
            _current = value;
            return _endValue;
        }

        _position.Index++;
        if (value == '\n')
        {
            _position.Line++;
            _position.Column = 0;
        }
        else
        {
            _position.Column++;
        }

        _current = _target[_position.Index];
        return _current;
    }

    public char Peek(int offset)
    {
        var index = _position.Index + offset;

        if (index >= _target.Length || index < 0)
            return _endValue;

        return  _target[index];
    }

    public bool IsEnded()
    {
        return _current == _endValue;
    }

    public IPosition Position()
    {
        return _position.Clone();
    }
}