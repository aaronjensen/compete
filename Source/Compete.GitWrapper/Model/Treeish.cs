using System;

namespace Compete.GitWrapper.Model
{
  public class Treeish
  {
    private readonly string _value;

    public string Value
    {
      get { return _value; }
    }

    public Treeish(string value)
    {
      _value = value;
    }
  }
}