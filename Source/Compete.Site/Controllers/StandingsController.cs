using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Site.Infrastructure;
using Compete.TeamManagement;
using Newtonsoft.Json;

namespace Compete.Site.Controllers
{
  public class StandingsController : CompeteController
  {
    private readonly ITeamManagementQueries _teamManagementQueries;
    private readonly IFormsAuthentication _formsAuthentication;

    public StandingsController(ITeamManagementQueries teamManagementQueries, IFormsAuthentication formsAuthentication)
    {
      _teamManagementQueries = teamManagementQueries;
      _formsAuthentication = formsAuthentication;
    }

    public ActionResult Index()
    {
      ViewData["TeamStandings"] = JavaScriptConvert.SerializeObject(_teamManagementQueries.GetTeamStandings());
      ViewData["Username"] = _formsAuthentication.IsCurrentlySignedIn ? _formsAuthentication.SignedInUserName : "not signed in!";
      return View();
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Latest()
    {
      var standings = _teamManagementQueries.GetTeamStandings();
      return Json(standings);
    }
  }
}
