using System;
using System.Collections.Generic;
using System.Text;

namespace Compete.Core
{
  public static class MoreEnumerables
  {
    public static string Join(this IEnumerable<string> collection, string separator)
    {
      StringBuilder sb = new StringBuilder();
      foreach (string value in collection)
      {
        if (sb.Length > 0)
        {
          sb.Append(separator);
        }
        sb.Append(value);
      }
      return sb.ToString();
    }

    public static IEnumerable<O> SelectWithIndex<I, O>(this IEnumerable<I> collection, Func<int, I, O> func)
    {
      int index = 0;
      IEnumerator<I> enumerator = collection.GetEnumerator();
      while (enumerator.MoveNext())
      {
        yield return func(index, enumerator.Current);
        index++;
      }
    }
  }
}
