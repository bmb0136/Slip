namespace Slip.Parser;

public static class Parser
{
  public static (int, ExprAST, ParserError?) ParseExpr(IReadOnlyList<Token> tokens, int start = 0)
  {
    Token t = tokens[start];
    return t.Type switch
    {
      TokenType.Int => (1, new IntExpr(int.Parse(t.Value), t.Start, t.End), null),
      TokenType.Float => (1, new FloatExpr(float.Parse(t.Value), t.Start, t.End), null),
      TokenType.String => (1, new StringExpr(t.Value, t.Start, t.End), null),
      TokenType.True => (1, new BoolExpr(true, t.Start, t.End), null),
      TokenType.False => (1, new BoolExpr(false, t.Start, t.End), null),
      TokenType.LParen => ParseGroup(tokens, start),
      TokenType.Let => ParseLet(tokens, start),
      TokenType.Identifier => ParseVariableOrFuncCall(tokens, start),
      TokenType.Match => ParseMatch(tokens, start),
      TokenType.Func => ParseLambda(tokens, start),
      _ => (-1, default!, new ParserError())
    };
  }

  private static (int, GroupExpr, ParserError?) ParseGroup(IReadOnlyList<Token> tokens, int start = 0)
  {
    Token lParen = tokens[start];

    int? maybeRParenIndex = tokens.FindMatching(start, TokenType.LParen, TokenType.RParen, static t => t.Type);
    if (maybeRParenIndex is not int rParenIndex)
    {
      return (-1, null!, new(ParserErrorType.MismatchedDelimeter, lParen.Start, lParen.End));
    }

    var (n, inner, error) = ParseExpr(tokens, start + 1);
    if (error is not null)
    {
      return (-1, null!, error);
    }

    return (n + 2, new GroupExpr(inner as ExprAST, lParen.Start, tokens[rParenIndex].End), null);
  }

  private static (int, LetExpr, ParserError?) ParseLet(IReadOnlyList<Token> tokens, int start = 0)
  {
    Token let = tokens[start];
    if (start + 1 >= tokens.Count || tokens[start + 1].Type != TokenType.Identifier)
    {
      return (-1, null!, new(ParserErrorType.ExpectedIdentifier, let.End, let.End + 1));
    }

    Token name = tokens[start + 1];
    if (start + 2 >= tokens.Count)
    {
      return (-1, null!, new(ParserErrorType.ExpectedEquals, name.End, name.End + 1));
    }

    var (valueSize, value, error) = ParseExpr(tokens, start + 3);
    if (error is not null)
    {
      return (-1, null!, error);
    }

    (int tailSize, ExprAST tail, error) = ParseExpr(tokens, start + 3 + valueSize);
    if (error is not null)
    {
      return (-1, null!, error);
    }

    return (3 + valueSize + tailSize, new LetExpr(name.Value, value, tail, let.Start), null);
  }

  private static (int, ExprAST, ParserError?) ParseVariableOrFuncCall(IReadOnlyList<Token> tokens, int start = 0)
  {
    throw new NotImplementedException();
  }

  private static (int, MatchExpr, ParserError?) ParseMatch(IReadOnlyList<Token> tokens, int start = 0)
  {
    throw new NotImplementedException();
  }

  private static (int, LambdaExpr, ParserError?) ParseLambda(IReadOnlyList<Token> tokens, int start = 0)
  {
    throw new NotImplementedException();
  }
}
