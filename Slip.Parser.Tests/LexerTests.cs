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
  [InlineData("rec", TokenType.Rec)]
  [InlineData("enum", TokenType.Enum)]
  [InlineData("let", TokenType.Let)]
  [InlineData("match", TokenType.Match)]
  [InlineData("_", TokenType.Discard)]
  public void Lex_Keywords_DoesNotReturnIdentifier(string code, TokenType token)
  {
    var (tokens, error) = Lexer.Lex(code);

    Assert.Null(error);
    Assert.Single(tokens);
    Assert.Equal(token, tokens[0].Type);
  }

  [Theory]
  [InlineData("_func")]
  [InlineData("mAtch")]
  [InlineData("letttt")]
  [InlineData("eenum")]
  [InlineData("hello_world")]
  [InlineData("h3ll0_W0RLD")]
  public void Lex_Identifier_ReturnsIdentifier(string code)
  {
    var (tokens, error) = Lexer.Lex(code);

    Assert.Null(error);
    Assert.Single(tokens);
    Assert.Equal(TokenType.Identifier, tokens[0].Type);
  }

  [Theory]
  [InlineData("123", 123)]
  [InlineData("0123", 123)]
  [InlineData("-289742", -289742)]
  [InlineData("123456789", 123456789)]
  public void Lex_Int_ReturnsInteger(string code, int value)
  {
    var (tokens, error) = Lexer.Lex(code);

    Assert.Null(error);
    Assert.Single(tokens);
    Assert.Equal(TokenType.Int, tokens[0].Type);
    Assert.True(int.TryParse(tokens[0].Value, out int parsed));
    Assert.Equal(value, parsed);
  }

  [Theory]
  [InlineData("1.23", 1.23)]
  [InlineData("0.123", 0.123)]
  [InlineData("-.289742", -.289742)]
  [InlineData("12345.6789", 12345.6789)]
  public void Lex_Float_ReturnsFloat(string code, double value)
  {
    var (tokens, error) = Lexer.Lex(code);

    Assert.Null(error);
    Assert.Single(tokens);
    Assert.Equal(TokenType.Float, tokens[0].Type);
    Assert.True(double.TryParse(tokens[0].Value, out double parsed));
    Assert.Equal(value, parsed);
  }
}
