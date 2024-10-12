namespace Slip.Parser.Tests;

public sealed class MatchParserTests()
{
  [Theory]
  [InlineData("match x (1 = 2, 3 = 4, _ = 5)")]
  public void ParseExpr_Match_ReturnsMatch(string code)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<MatchExpr>(expr);
  }
}
