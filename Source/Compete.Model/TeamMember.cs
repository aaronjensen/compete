using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compete.Model
{
  public class TeamMember
  {
    public string Name { get; private set; }
    public TeamMember (string name)
    {
      Name = name;
    }
  }
}