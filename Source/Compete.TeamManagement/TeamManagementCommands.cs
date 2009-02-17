using System;
using Compete.Model;
using Compete.Model.Repositories;

namespace Compete.TeamManagement
{
  public interface ITeamManagementCommands
  {
    bool New(string teamName, string longName);
  }

  public class TeamManagementCommands : ITeamManagementCommands
  {
    private readonly ITeamRepository _repository;

    public TeamManagementCommands(ITeamRepository teamRepository)
    {
      _repository = teamRepository;
    }

    public bool New(string teamName, string longName)
    {
      Team team;
      try
      {
        team = new Team(teamName, longName, new TeamMember[0]);
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