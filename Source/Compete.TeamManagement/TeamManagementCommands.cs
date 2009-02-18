using System;
using System.Collections.Generic;
using System.Linq;
using Compete.Model;
using Compete.Model.Repositories;

namespace Compete.TeamManagement
{
  public interface ITeamManagementCommands
  {
    bool New(string teamName, string longName, IEnumerable<string> teamMembers);
  }

  public class TeamManagementCommands : ITeamManagementCommands
  {
    private readonly ITeamRepository _repository;

    public TeamManagementCommands(ITeamRepository teamRepository)
    {
      _repository = teamRepository;
    }

    public bool New(string teamName, string longName, IEnumerable<string> teamMembers)
    {
      var members = teamMembers.Select(x => new TeamMember(x));
      Team team;
      try
      {
        team = new Team(teamName, longName, members);
      }
      catch (ArgumentException e)
      {
        return false;
      }
      _repository.Add(team);
      return true;
    }
  }
}