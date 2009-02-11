using System;
using System.Collections.Generic;

namespace Compete.GitWrapper.Model
{
  public class Branch
  {
    private readonly string _name;
    private readonly string _lastCommitMessage;

    public string Name
    {
      get { return _name; }
    }

    public string LastCommitMessage
    {
      get { return _lastCommitMessage; }
    }

    public Branch(string name, string lastCommitMessage)
    {
      _name = name;
      _lastCommitMessage = lastCommitMessage;
    }

    public override string ToString()
    {
      return "Branch<" + _name + ">";
    }
  }
}
