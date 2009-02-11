using System;

namespace Compete.GitWrapper.Model
{
  public class GitHash
  {
    private readonly string _value;

    public GitHash(string value)
    {
      _value = value;
    }

    public override string ToString()
    {
      return _value;
    }

    public override bool Equals(object obj)
    {
      if (obj is GitHash)
      {
        return ((GitHash)obj)._value.Equals(_value);
      }
      return false;
    }

    public override Int32 GetHashCode()
    {
      return _value.GetHashCode();
    }
  }
}