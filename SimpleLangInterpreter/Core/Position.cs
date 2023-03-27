namespace SimpleLangInterpreter.Core;

public class Position
{
    public int Index { get;  set; }
    public int Column { get;private set;  }
    public int Line { get; private set; }

    public string FileName { get; }

    public string Content { get; }

    public Position(int index, int column, int line, string fileName, string content)
    {
        Index = index;
        Column = column;
        Line = line;
        FileName = fileName;
        Content = content;
    }

    public void advance(char? _char)
    {
        Index++;
        Column++;

        if (_char == null)
        {
            return;
        }

        if (_char is '\n' or '\r')
        {
            Line++;
            Column = 0;
        }
    }

    public Position copy()
    {
        return new Position(Index, Column, Line, FileName, Content);
    }

    public override string ToString()
    {
        return $"in file {FileName} at line: {Line}, column {Column}";
    }
}