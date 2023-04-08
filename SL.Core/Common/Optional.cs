namespace SL.Core.Common;

public class Optional<T>
{
    private T value;
    public bool IsPresent { get; private set; } = false;

    private Optional()
    {
    }

    public static Optional<T> Empty()
    {
        return new Optional<T>();
    }

    public static Optional<T> Of(T value)
    {
        Optional<T> obj = new Optional<T>();
        obj.Set(value);
        return obj;
    }


    public static implicit operator Optional<T>(T value)
    {
        return Of(value);
    }

    public void Set(T value)
    {
        this.value = value;
        IsPresent = true;
    }

    public T Get()
    {
        return value;
    }
}