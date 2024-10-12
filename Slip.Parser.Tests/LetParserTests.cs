namespace Slip.Parser.Tests;

public sealed class LetParserTests
{
  [Theory]
  [InlineData("let x = 69 true", "x")]
  public void ParseExpr_Let_ReturnsLet(string code, string name)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<LetExpr>(expr);
    Assert.Equal(name, (expr as LetExpr)!.Name);
  }
}
