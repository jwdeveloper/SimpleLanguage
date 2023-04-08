namespace SL.Core.Api;

public interface IPosition
{
    public int Index { get; }
    public int Line { get; }
    public int Column { get; }
    public IPosition Clone();
}