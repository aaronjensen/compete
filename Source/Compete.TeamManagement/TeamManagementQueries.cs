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
  }
}
