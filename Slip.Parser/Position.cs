namespace Slip.Parser;

public readonly record struct Position(int Line, int Column)
{
  public static Position operator+(Position p, int x) => p with { Column = p.Column + x };
  public static Position operator+(Position p, long x) => p with { Line = (int)(p.Line + x) };
}
