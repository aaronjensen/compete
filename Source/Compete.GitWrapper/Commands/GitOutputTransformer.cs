using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Compete.GitWrapper.Commands
{
  public abstract class GitOutputTransformer<TType>
  {
    private class Matcher
    {
      public Regex Re { get; private set; }
      public TransformLine Transformer { get; private set; }
      public Matcher(Regex re, TransformLine transformer)
      {
        this.Re = re;
        this.Transformer = transformer;
      }
      public TType Apply(string line)
      {
        Match match = this.Re.Match(line.Trim());
        if (!match.Success)
        {
          return default(TType);
        }
        return this.Transformer(line, match);
      }
    }
    private readonly List<Matcher> _matchers = new List<Matcher>();

    public delegate TType TransformLine(string line, Match match);

    public void Add(string re, TransformLine transformer)
    {
      _matchers.Add(new Matcher(new Regex(re), transformer));
    }

    public IEnumerable<TType> Transform(IEnumerable<string> lines)
    {
      foreach (string line in lines)
      {
        foreach (Matcher matcher in _matchers)
        {
          TType returned = matcher.Apply(line);
          if (!Equals(default(TType), returned))
          {
            yield return returned;
          }
        }
      }
    }
  }
}