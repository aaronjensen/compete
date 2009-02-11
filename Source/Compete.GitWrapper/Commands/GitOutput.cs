using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Compete.GitWrapper.Commands
{
  public class GitOutput
  {
    private readonly string _text;

    public GitOutput(string text)
    {
      _text = text;
    }

    public IEnumerable<string> SplitAndTrimLines()
    {
      if (_text.Length > 0)
      {
        foreach (string line in _text.Split('\n'))
        {
          yield return line.Trim();
        }
      }
    }

    public IEnumerable<string> SelectColumn(int index)
    {
      Regex ws = new Regex(@"\s+");
      foreach (string line in SplitAndTrimLines())
      {
        yield return ws.Split(line)[index];
      }
    }

    public IEnumerable<TType> Transform<TType>(GitOutputTransformer<TType> transformer)
    {
      return transformer.Transform(SplitAndTrimLines());
    }

    public override string ToString()
    {
      return _text;
    }
  }
}