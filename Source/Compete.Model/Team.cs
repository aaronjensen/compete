using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Compete.Model
{
  public class Team : Entity
  {
    string _teamName;
    private readonly string _displayName;
    readonly string _password;
    readonly List<TeamMember> _teamMembers;
    readonly Regex nonWordCharacters = new Regex(@"\W+");

    public string Name { get { return _teamName; } }
    public string DisplayName { get { return _displayName; } }
    public IEnumerable<TeamMember> TeamMembers { get { return _teamMembers; } }

    public Team(string teamName, string longName, IEnumerable<TeamMember> teamMembers, string passwordHash)
    {
      if (!IsValidTeamName(teamName))
      {
        throw new ArgumentException("Team name must be less than 16 characters and consist of only numbers and letters");
      }
      _teamName = teamName;
      _displayName = longName;
      _password = passwordHash;
      _teamMembers = new List<TeamMember>(teamMembers);
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
  }
}
