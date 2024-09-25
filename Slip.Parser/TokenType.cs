namespace Slip.Parser;

public enum TokenType
{
  Unknown = 0,
  Comment,
  Identifier,
  Discard,

  // Literals
  Int,
  Float,
  String,
  True,
  False,

  // Symbols
  LParen,
  RParen,
  Equals,
  DoubleColon,

  // Keywords
  Func,
  Let,
  Rec,
  Match
}
