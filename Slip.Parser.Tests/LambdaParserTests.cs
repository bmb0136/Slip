namespace Slip.Parser.Tests;

public sealed class LambdaParserTests
{
  [Theory]
  public void ParseExpr_Lambda_ReturnsLambda(string code)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<LambdaExpr>(expr);
  }
}
