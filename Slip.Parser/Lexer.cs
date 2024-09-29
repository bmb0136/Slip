using System.Text;

namespace Slip.Parser;

public static class Lexer
{
  public static (IReadOnlyList<Token>, ParserError?) Lex(ReadOnlySpan<char> code)
  {
    Span<char> lookahead = stackalloc char[3];
    List<Token> tokens = [];
    Position pos = new(1, 1);
    while (code.Length > 0)
    {
      if (code[0] == '\r')
      {
        code = code[1..];
        continue;
      }
      if (code[0] == '\n')
      {
        pos = pos with { Column = 1 } + 1L;
        code = code[1..];
      }

      lookahead.Clear();
      code[..Math.Min(code.Length, lookahead.Length)].CopyTo(lookahead);

      (int read, Token token, ParserError? error) = lookahead switch
      {
        ['_', ..] => (1, new Token(TokenType.Discard, "_", pos, pos + 1), default),
        ['(', ..] => (1, new Token(TokenType.LParen, "(", pos, pos + 1), default),
        [')', ..] => (1, new Token(TokenType.RParen, ")", pos, pos + 1), default),
        ['=', ..] => (1, new Token(TokenType.Equals, "=", pos, pos + 1), default),
        [':', ':', ..] => (2, new Token(TokenType.DoubleColon, "::", pos, pos + 2), default),
        ['/', '/', ..] => LexComment(code),
        ['"', ..] => LexString(code, pos),
        [>= '0' and <= '9' or '.' or '-', ..] => LexNumber(code),
        [>= 'a' and <= 'z' or >= 'A' and <= 'Z' or '_', ..] => LexIdentifier(code),
        _ => (0, default(Token), new ParserError(ParserErrorType.SyntaxError, pos, pos + 1))
      };

      if (error is not null)
      {
        return (null!, error);
      }

      tokens.Add(token);
      code = code[read..];
    }

    return (tokens, null);
  }

  private static (int, Token, ParserError?) LexComment(ReadOnlySpan<char> code)
  {
    throw new NotImplementedException();
  }

  private static (int, Token, ParserError?) LexNumber(ReadOnlySpan<char> code)
  {
    throw new NotImplementedException();
  }

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

  private static (int, Token, ParserError?) LexIdentifier(ReadOnlySpan<char> code)
  {
    throw new NotImplementedException();
  }

}
