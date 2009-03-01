using System;
using System.Collections.Generic;
using System.Linq;

using Compete.Site.Models;
using Compete.Site.Refereeing;
using Compete.TeamManagement;

namespace Compete.Site.Infrastructure
{
  public class MatchStarter
  {
    readonly AssemblyFileRepository _assemblyFileRepository = new AssemblyFileRepository();
    readonly IRefereeThread _refereeThread;
    readonly ITeamManagementQueries _teamManagementQueries;

    public MatchStarter(IRefereeThread refereeThread, ITeamManagementQueries teamManagementQueries)
    {
      _refereeThread = refereeThread;
      _teamManagementQueries = teamManagementQueries;
    }

    public void Queue(string teamName)
    {
      var teamNames = _teamManagementQueries.GetAllTeamNames();
      Queue(teamNames.ToArray());
    }

    public void QueueForAll()
    {
      var teamNames = _teamManagementQueries.GetAllTeamNames();
      Queue(teamNames.ToArray());
    }

    private void Queue(string[] teamNames)
    {
      var parameters = new RoundParameters(_assemblyFileRepository.FindAllGamesAndPlayers().ToArray(), teamNames);
      _refereeThread.Start(parameters);
    }
  }
}
