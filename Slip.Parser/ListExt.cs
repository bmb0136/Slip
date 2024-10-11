namespace Slip.Parser;

internal static class ListExt
{
  public static int? FindMatching<T, U>(this IReadOnlyList<T> list, int start, U opener, U closer, Func<T, U> map) where U : IComparable
  {
    int depth = 0;
    for (int i = start; i < list.Count; i++)
    {
      var u = map(list[i]);
      if (opener.CompareTo(u) == 0)
      {
        depth++;
      }
      else if (closer.CompareTo(u) == 0)
      {
        depth--;
      }
      if (depth == 0)
      {
        return i;
      }
    }
    return null;
  }
}
