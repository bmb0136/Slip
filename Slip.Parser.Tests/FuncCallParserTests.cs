namespace Slip.Parser.Tests;

public sealed class FuncCallParserTests
{
  [Theory]
  public void ParseExpr_FuncCall_ReturnsFuncCall(string code)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<FuncCallExpr>(expr);
  }
}
