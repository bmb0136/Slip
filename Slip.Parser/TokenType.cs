namespace Slip.Parser;

public enum TokenType
{
  Unknown = 0,
  Comment,
  Identifier,

  // Literals
  Int,
  Float,
  String,
  True,
  False,

  // Symbols
  LParen,
  RParen,
  Comma,
  Equals,
  DoubleColon,

  // Keywords
  Func,
  Let,
  Rec,
  Match
}
