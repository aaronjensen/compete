using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Compete.Site.Filters;
using Compete.Site.Infrastructure;
using Compete.Site.Refereeing;

namespace Compete.Site.Controllers
{
  [RequireAdministratorPrivilegesFilter]
  public class CompetitionController : CompeteController
  {
    readonly MatchStarter _matchStarter;

    public CompetitionController(MatchStarter matchStarter)
    {
      _matchStarter = matchStarter;
    }

    public ActionResult Index()
    {
      _matchStarter.QueueForAll();
      return RedirectToReferrer();
    }
  }

  public class MatchController : CompeteController
  {
    readonly IRefereeThread _refereeThread;

    public MatchController(IRefereeThread refereeThread)
    {
      _refereeThread = refereeThread;
    }

    public ActionResult Status()
    {
      return Json(_refereeThread.IsRunning);
    }
  }
}
