using System;
using Compete.Model;
using Compete.Model.Repositories;

namespace TeamManagement
{
  public interface ITeamManagementService
  {
    bool New(string teamName);
  }

  public class TeamManagementService : ITeamManagementService
  {
    private readonly ITeamRepository _repository;

    public TeamManagementService(ITeamRepository teamRepository)
    {
      _repository = teamRepository;
    }

    public bool New(string teamName)
    {
      Team team;
      try
      {
        team = new Team(teamName);
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