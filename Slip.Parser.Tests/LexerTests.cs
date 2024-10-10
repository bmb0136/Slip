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
    Assert.Single(tokens);
    Assert.Equal(type, tokens[0].Type);
  }

  [Theory]
  [InlineData("\"hello\"", "hello")]
  [InlineData("\"test\\n123\"", "test\n123")]
  [InlineData("\"\\\"\\\\\\n\\r\\t\\0\"", "\"\\\n\r\t\0")]
  public void Lex_String_ReturnsCorrectValue(string code, string value)
  {
    var (tokens, error) = Lexer.Lex(code);

    Assert.Null(error);
    Assert.Single(tokens);
    Assert.Equal(value, tokens[0].Value);
  }

  [Theory]
  [InlineData("func", TokenType.Func)]
  [InlineData("rec", TokenType.Func)]
  [InlineData("let", TokenType.Func)]
  [InlineData("match", TokenType.Func)]
  public void Lex_Keywords_DoesNotReturnIdentifier(string code, TokenType token)
  {
    var (tokens, error) = Lexer.Lex(code);

    Assert.Null(error);
    Assert.Single(tokens);
    Assert.Equal(token, tokens[0].Type);
  }
}
