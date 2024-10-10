
namespace Slip.Parser;

public static partial class Lexer
{
  private static (int, Token, ParserError?) LexComment(ReadOnlySpan<char> code, Position start)
  {
    int end = code.IndexOf('\n');
    if (end < 0)
    {
      end = code.Length;
    }
    return (end, new(TokenType.Comment, new(code[..end]), start, start + end), null);
  }
}
