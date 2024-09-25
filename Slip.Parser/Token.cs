namespace Slip.Parser;

public readonly record struct Token(TokenType Type, string Value, Position Start, Position End);
