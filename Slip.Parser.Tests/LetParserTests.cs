namespace Slip.Parser.Tests;

public sealed class LetParserTests
{
  [Theory]
  public void ParseExpr_Let_ReturnsLet(string code)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<LetExpr>(expr);
  }
}
