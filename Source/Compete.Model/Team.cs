using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Compete.Model
{
  public class Team : Entity
  {
    string _teamName;
    private readonly string _longName;
    readonly string _password;
    readonly List<TeamMember> _teamMembers;
    readonly Regex nonWordCharacters = new Regex(@"\W+");

    public string Name { get { return _teamName; } }
    public string LongName { get { return _longName; } }
    public IEnumerable<TeamMember> TeamMembers { get { return _teamMembers; } }

    public List<TeamStanding> _standings;
    public IEnumerable<TeamStanding> Standings
    {
      get { return _standings; }
    }

    public Team(string teamName, string longName, IEnumerable<TeamMember> teamMembers, string passwordHash)
    {
      if (!IsValidTeamName(teamName))
      {
        throw new ArgumentException("Team name must be less than 16 characters and consist of only numbers and letters");
      }
      _teamName = teamName;
      _longName = longName;
      _password = passwordHash;
      _teamMembers = new List<TeamMember>(teamMembers);

      _standings = new List<TeamStanding>();
    }

    private bool IsValidTeamName(string name)
    {
      if (name.Length > 16)
      {
        return false;
      }
      if (nonWordCharacters.IsMatch(name))
      {
        return false;
      }
      if (name.Equals(string.Empty))
      {
        return false;
      }
      
      return true;
    }

    public void AddTeamMember(TeamMember teamMember)
    {
      _teamMembers.Add(teamMember);
    }

    public void RemoveTeamMember(TeamMember teamMember)
    {
      _teamMembers.Remove(teamMember);
    }

    public bool Authenticate(string passwordHash)
    {
      return passwordHash == _password;
    }

    public void AddStanding(int rank, int wins, int losses, int ties)
    {
      var lastStanding = _standings.LastOrDefault();
      var lastChange = 0;
      if (lastStanding != null)
      {
        lastChange = rank - lastStanding.Rank;
      }
        
      var standing = new TeamStanding(rank, wins, losses, ties, lastChange);
      _standings.Add(standing);
    }
  }
}
