using SL.Core.Api;

namespace SL.Core.Common;

public class Position  : IPosition
{
    public int Index { get; set; }
    public int Line { get; set; }
    public int Column { get; set; }
    
    public Position(int Index = -1, int Line = 0, int Column = -1)
    {
        this.Index = Index;
        this.Line = Line;
        this.Column = Column;
    }

    public IPosition Clone()
    {
        return new Position(Index, Line, Column);
    }
  
}