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
  }
}
