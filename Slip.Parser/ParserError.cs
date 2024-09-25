namespace Slip.Parser;

public readonly record struct ParserError(ParserErrorType Type, Position Start, Position End);
