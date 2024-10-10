namespace Slip.Parser;

public abstract record AST(Position Start, Position End);

public abstract record ExprAST(Position Start, Position End) : AST(Start, End);

public sealed record LetDefAST(string Name, ExprAST Value, Position Start, Position End) : AST(Start, End);

public sealed record RecDefAST(string Name, IReadOnlyList<string> Fields, Position Start, Position End) : AST(Start, End);

public sealed record EnumDefAST(string Name, IReadOnlyList<EnumVariantAST> Variants, Position Start, Position End) : AST(Start, End);
public sealed record EnumVariantAST(string Name, IReadOnlyList<string> Fields, Position Start, Position End) : AST(Start, End);

public sealed record FuncDefAST(string Name, IReadOnlyList<string> Arguments, ExprAST Body, Position Start, Position End) : AST(Start, End);

public sealed record IntExpr(long Value, Position Start, Position End) : ExprAST(Start, End);
public sealed record FloatExpr(double Value, Position Start, Position End) : ExprAST(Start, End);
public sealed record StringExpr(string Value, Position Start, Position End) : ExprAST(Start, End);
public sealed record BoolExpr(bool Value, Position Start, Position End) : ExprAST(Start, End);

public sealed record LetExpr(string Name, ExprAST Value, ExprAST Tail, Position Start) : ExprAST(Start, Tail.End);

public sealed record QualifierAST(IReadOnlyList<string> Parts, Position Start, Position End) : AST(Start, End);

public sealed record VariableExpr(QualifierAST Name) : ExprAST(Name.Start, Name.End);

public sealed record FuncCallExpr(QualifierAST Name, IReadOnlyList<ExprAST> Arguments, Position Start, Position End) : ExprAST(Start, End);

public sealed record MatchExpr(ExprAST Value, IReadOnlyList<MatchArmAST> Arms, Position Start, Position End) : ExprAST(Start, End);
public sealed record MatchArmAST(ExprAST Condition, ExprAST Value, Position Start, Position End) : AST(Start, End);
