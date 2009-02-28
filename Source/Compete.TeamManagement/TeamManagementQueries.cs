using System;
using System.Collections.Generic;
using System.Linq;
using Compete.Model;
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
    readonly ITeamRepository _teamRepository;
    readonly ILeaderboardRepository _leaderboardRepository;

    public TeamManagementQueries(ITeamRepository teamRepository, ILeaderboardRepository leaderboardRepository)
    {
      _teamRepository = teamRepository;
      _leaderboardRepository = leaderboardRepository;
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
      var leaderboard = _leaderboardRepository.GetLeaderboard();
      return leaderboard.ToStandingSummary(teams.Select(x => x.Name));
    }

    public IEnumerable<string> GetAllTeamNames()
    {
      var teams = _teamRepository.GetAllTeams();
      return teams.Select(x => x.Name);
    }
  }
}