using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Compete.Site.Filters;
using Compete.Site.Models;
using Compete.Site.Refereeing;
using Compete.TeamManagement;

namespace Compete.Site.Controllers
{
  [RequireAdministratorPrivilegesFilter]
  public class CompetitionController : CompeteController
  {
    readonly AssemblyFileRepository _assemblyFileRepository = new AssemblyFileRepository();
    readonly IRefereeThread _refereeThread;
    readonly ITeamManagementQueries _teamManagementQueries;

    public CompetitionController(IRefereeThread refereeThread, ITeamManagementQueries teamManagementQueries)
    {
      _refereeThread = refereeThread;
      _teamManagementQueries = teamManagementQueries;
    }

    public ActionResult Index()
    {
      var teamNames = _teamManagementQueries.GetAllTeamNames();
      var referee = new Referee(_assemblyFileRepository.FindAllGamesAndPlayers().ToArray(),teamNames);
      _refereeThread.Start(referee);
      return RedirectToReferrer();
    }
  }
}
