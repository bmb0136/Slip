namespace Slip.Parser;

public static partial class Lexer
{
  private static (int, Token, ParserError?) LexIdentifier(ReadOnlySpan<char> code, Position start)
  {
    int n = 0;

    while (n < code.Length && code[n] is (>= 'A' and <= 'Z') or (>= 'a' and <= 'z') or (>= '0' and <= '9') or '_')
    {
      n++;
    }

    var value = code[..n];

    TokenType type = value switch
    {
      "func" => TokenType.Func,
      "let" => TokenType.Let,
      "rec" => TokenType.Rec,
      "enum" => TokenType.Enum,
      "match" => TokenType.Match,
      _ => TokenType.Identifier
    };

    return (n, new Token(type, new(value), start, start + n), null);
  }
}
