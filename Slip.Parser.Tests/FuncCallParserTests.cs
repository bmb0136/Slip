namespace Slip.Parser.Tests;

public sealed class FuncCallParserTests
{
  [Theory]
  [InlineData("print(69)", new[] { "print" })]
  [InlineData("math::sqrt(69)", new[] { "math", "sqrt" })]
  public void ParseExpr_FuncCall_ReturnsFuncCall(string code, string[] parts)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<FuncCallExpr>(expr);
    Assert.Equal(parts, (expr as FuncCallExpr)!.Name.Parts);
  }
}
