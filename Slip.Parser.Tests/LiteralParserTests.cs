namespace Slip.Parser.Tests;

public sealed class LiteralParserTests
{
  [Theory]
  [InlineData("1234", 1234)]
  [InlineData("-5678", -5678)]
  public void ParseExpr_Integer_ReturnsInt(string code, int x)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<IntExpr>(expr);
    Assert.Equal(x, (expr as IntExpr)?.Value);
  }

  [Theory]
  [InlineData("12.34", 12.34)]
  [InlineData("-56.78", -56.78)]
  public void ParseExpr_Float_ReturnsFloat(string code, double x)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<FloatExpr>(expr);
    Assert.True(Math.Abs(x - (expr as FloatExpr)!.Value) < 0.001);
  }

  [Theory]
  [InlineData("true", true)]
  [InlineData("false", false)]
  public void ParseExpr_Boolean_ReturnsBool(string code, bool x)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<BoolExpr>(expr);
    Assert.Equal(x, (expr as BoolExpr)?.Value);
  }

  [Theory]
  [InlineData("\"hello\"", "hello")]
  [InlineData("\"world\\n1234\"", "world\n1234")]
  public void ParseExpr_String_ReturnsString(string code, string x)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<StringExpr>(expr);
    Assert.Equal(x, (expr as StringExpr)?.Value);
  }
}
