using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compete.Model.Reports
{
  public class TeamSummary
  {
    public string Name { get; set; }
    public string LongName { get; set; }
    public IEnumerable<string> TeamMembers { get; set; }

    public TeamSummary(string teamName, string longName, IEnumerable<string> teamMembers)
    {
      Name = teamName;
      LongName = longName;
      TeamMembers = teamMembers;
    }
  }
}
