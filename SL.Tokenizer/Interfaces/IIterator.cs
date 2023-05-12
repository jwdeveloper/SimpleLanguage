namespace SL.Tokenizer.Interfaces;

public interface IIterator<T>
{
    public T Current();

    public T Advance();

    public T Peek(int offset);

    public bool IsEnded();

    public IPosition Position();
}