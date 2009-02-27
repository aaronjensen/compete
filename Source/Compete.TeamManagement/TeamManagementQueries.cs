using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.Model.Reports;
using Compete.Model.Repositories;

namespace Compete.TeamManagement
{
  public interface ITeamManagementQueries
  {
    IEnumerable<TeamSummary> GetTeamSummaries();
    bool TeamNameIsAvailable(string name);
    IEnumerable<TeamStandingSummary> GetTeamStandings();
    IEnumerable<string> GetAllTeamNames();
  }

  public class TeamManagementQueries : ITeamManagementQueries
  {
    private readonly ITeamRepository _teamRepository;

    public TeamManagementQueries(ITeamRepository teamRepository)
    {
      _teamRepository = teamRepository;
    }

    public IEnumerable<TeamSummary> GetTeamSummaries()
    {
      return _teamRepository.GetAllTeams().Select(team=>new TeamSummary(team.Name, team.LongName, team.TeamMembers.Select(teamMember => teamMember.Name)));
    }

    public bool TeamNameIsAvailable(string name)
    {
      var team = _teamRepository.FindByTeamName(name);
      return team == null;
    }

    public IEnumerable<TeamStandingSummary> GetTeamStandings()
    {
      var teams = _teamRepository.GetAllTeams();
      return teams.Select(x =>
                           {
                             var standing = x.Standings.LastOrDefault();
                             if (standing == null)
                             {
                               return new TeamStandingSummary(x.Name, 0, 0, 0, 0, 0);
                             }
                             return new TeamStandingSummary(
                               x.Name, 
                               standing.Rank, 
                               standing.Wins, 
                               standing.Losses, 
                               standing.Ties, 
                               standing.LastChange);
                           });
    }

    public IEnumerable<string> GetAllTeamNames()
    {
      var teams = _teamRepository.GetAllTeams();
      return teams.Select(x => x.Name);
    }
  }
}