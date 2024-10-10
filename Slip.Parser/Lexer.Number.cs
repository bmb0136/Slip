namespace Slip.Parser;

public static partial class Lexer
{
  private static (int, Token, ParserError?) LexNumber(ReadOnlySpan<char> code, Position start)
  {
    bool seenDot = code[0] == '.';
    int n = 0;

    while (n < code.Length && code[n] is (>= '0' and <= '9') or '.' or '-')
    {
      if (code[n] == '-' && n > 0)
      {
        return (-1, default, new(ParserErrorType.MultipleMinusSingsInNumber, start + n, 1));
      }
      if (code[n] == '.')
      {
        if (seenDot)
        {
          return (-1, default, new(ParserErrorType.MultipleDecimalPointsInNumber, start + n, 1));
        }
        seenDot = true;
      }
      n++;
    }

    if (code.Length == 1 && (seenDot || code[0] == '-'))
    {
      return (-1, default, new(ParserErrorType.ExpectedNumber, start + 1, 1));
    }

    return (n, new(seenDot ? TokenType.Float : TokenType.Int, new(code[..n]), start, start + n), null);
  }
}
