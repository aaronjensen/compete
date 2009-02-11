using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compete.Model
{
  public class Team : Entity
  {
    string _teamName;
    readonly List<TeamMember> _teamMembers;

    public Team(string teamName)
    {
      _teamName = teamName;
      _teamMembers = new List<TeamMember>();
    }

    public void AddTeamMember(TeamMember teamMember)
    {
      _teamMembers.Add(teamMember);
    }

    public void RemoveTeamMember(TeamMember teamMember)
    {
      _teamMembers.Remove(teamMember);
    }
  }
}
