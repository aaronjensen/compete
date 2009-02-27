using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Compete.Site.Filters;
using Compete.Site.Models;
using Compete.Site.Refereeing;

namespace Compete.Site.Controllers
{
  [RequireAdministratorPrivilegesFilter]
  public class CompetitionController : CompeteController
  {
    readonly AssemblyFileRepository _assemblyFileRepository = new AssemblyFileRepository();
    readonly IRefereeThread _refereeThread;

    public CompetitionController(IRefereeThread refereeThread)
    {
      _refereeThread = refereeThread;
    }

    public ActionResult Index()
    {
      Referee referee = new Referee(_assemblyFileRepository.FindAllGamesAndPlayers().ToArray());
      _refereeThread.Start(referee);
      return RedirectToReferrer();
    }
  }
}
