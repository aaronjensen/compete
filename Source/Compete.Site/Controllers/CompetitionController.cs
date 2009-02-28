using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Compete.Site.Filters;
using Compete.Site.Infrastructure;

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
}
