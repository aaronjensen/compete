using System;
using System.Collections.Generic;
using System.Text;

namespace Compete.GitWrapper.Utilities
{
  public static class StringExtensions
  {
    public static string Join(this string[] array, string separator)
    {
      StringBuilder sb = new StringBuilder();
      foreach (string value in array)
      {
        if (sb.Length > 0)
        {
          sb.Append(separator);
        }
        sb.Append(value);
      }
      return sb.ToString();
    }

    public static string[] Format(this string[] array, string f)
    {
      List<string> strings = new List<string>();
      foreach (string value in array)
      {
        strings.Add(String.Format(f, value));
      }
      return strings.ToArray();
    }

    public static string Quote(this string value)
    {
      return '"' + value + '"';
    }
  }
}