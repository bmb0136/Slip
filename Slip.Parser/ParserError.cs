namespace Slip.Parser;

public readonly record struct ParserError(ParserErrorType Type, Position Start, Position End)
{
  public ParserError(ParserErrorType type, Position start, int offset) : this(type, start, start + offset)
  {
  }
}
