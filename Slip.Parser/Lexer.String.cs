using System.Text;

namespace Slip.Parser;

public static partial class Lexer
{
  private static (int, Token, ParserError?) LexString(ReadOnlySpan<char> code, Position start)
  {
    code = code[1..]; // Eat "
    int read = 1;
    StringBuilder value = new();
    Span<char> lookahead = stackalloc char[2];
    
    while (code[0] != '"')
    {
      lookahead.Clear();
      code[..Math.Min(code.Length, lookahead.Length)].CopyTo(lookahead);

      (int len, string? str, ParserError? error) = lookahead switch
      {
        ['\r' or '\n', ..] => (0, null, new ParserError(ParserErrorType.NoMultiLineStrings, start + read + 1, 1)),
        ['\\', char c] => c switch
        {
          'n' => (2, "\n", default(ParserError?)),
          'r' => (2, "\r", default(ParserError?)),
          't' => (2, "\t", default(ParserError?)),
          '0' => (2, "\0", default(ParserError?)),
          '\\' => (2, "\\", default(ParserError?)),
          '"' => (2, "\"", default(ParserError?)),
          _ => (0, null, new ParserError(ParserErrorType.UnknownEscapeSequence, start + read + 1, 1))
        },
        _ => (1, null, default)
      };
      
      if (error is not null)
      {
        return (0, default, error);
      }

      value.Append(str ?? code[..len]);
      code = code[len..];
      read += len;
    }

    read++;

    return (read, new(TokenType.String, value.ToString(), start, start + read), null);
  }
}
