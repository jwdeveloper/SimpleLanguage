namespace SL.Core.Common;

public class AbstractIterator<T>
{
    protected readonly List<T> _target;
    protected T _defaultValue;
    private T? _current;
    private int _position;

    public AbstractIterator(List<T> target, T defaultValue)
    {
        _target = target;
        _current = defaultValue;
        _position = -1;
        _defaultValue = defaultValue;
    }

    public T Current()
    {
        return _current;
    }

    public T Advance()
    {
        _position++;
        if (_position >= _target.Count)
        {
            _current = _defaultValue;
            return _defaultValue;
        }
        _current = _target[_position];
        return _current;
    }

    public T Peek(int offset)
    {
        var index = _position + offset;

        if (index >= _target.Count)
            return _defaultValue;
 
        return _target[index];
    }


    public bool IsValid()
    {
        return  _current != null && !_current.Equals(_defaultValue);
    }


    
    
}