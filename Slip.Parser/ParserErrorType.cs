namespace Slip.Parser;

public enum ParserErrorType
{
  Unknown = 0,
  SyntaxError,
  NoMultiLineStrings,
  UnknownEscapeSequence,
  MultipleMinusSingsInNumber,
  MultipleDecimalPointsInNumber,
  ExpectedNumber,
  MismatchedDelimeter,
  ExpectedIdentifier,
  ExpectedEquals
}
