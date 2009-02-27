using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.TeamManagement;
using Newtonsoft.Json;

namespace Compete.Site.Controllers
{
  public class StandingsController : CompeteController
  {
    private readonly ITeamManagementQueries _teamManagementQueries;

    public StandingsController(ITeamManagementQueries teamManagementQueries)
    {
      _teamManagementQueries = teamManagementQueries;
    }

    public ActionResult Index()
    {
      ViewData["TeamStandings"] = JavaScriptConvert.SerializeObject(_teamManagementQueries.GetTeamStandings());
      return View();
    }
  }
}
