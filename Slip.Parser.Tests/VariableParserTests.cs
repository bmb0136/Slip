namespace Slip.Parser.Tests;

public sealed class VariableParserTests
{
  [Theory]
  [InlineData("hello", new[] { "hello" } )]
  [InlineData("hello::world", new[] { "hello", "world" })]
  public void ParseExpr_Variable_ReturnsCorrectName(string code, string[] parts)
  {
    var (tokens, error) = Lexer.Lex(code);
    Assert.Null(error);

    (_, ExprAST expr, error) = Parser.ParseExpr(tokens);

    Assert.Null(error);
    Assert.IsType<VariableExpr>(expr);
    Assert.Equal(parts, (expr as VariableExpr)!.Name.Parts);
  }
}
