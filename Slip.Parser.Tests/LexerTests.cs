namespace Slip.Parser.Tests;

public class LexerTests
{
  [Theory]
  [InlineData("(", TokenType.LParen)]
  [InlineData(")", TokenType.RParen)]
  [InlineData("::", TokenType.DoubleColon)]
  [InlineData("=", TokenType.Equals)]
  [InlineData("_", TokenType.Discard)]
  public void Lex_BasicSymbols_ReturnsCorrectType(string code, TokenType type)
  {
    var (tokens, error) = Lexer.Lex(code);

    Assert.Null(error);
    Assert.Equal(1, tokens.Count);
    Assert.Equal(type, tokens[0].Type);
  }
}
