using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.TeamManagement;
using Newtonsoft.Json;

namespace Compete.Site.Controllers
{
  public class StandingsController : Controller
  {
    private readonly ITeamManagementQueries _teamManagementQueries;

    public StandingsController(ITeamManagementQueries teamManagementQueries)
    {
      _teamManagementQueries = teamManagementQueries;
    }

    public ActionResult Index()
    {
      var standings = _teamManagementQueries.GetTeamStandings();
      JavaScriptConvert.SerializeObject(standings);
      return View();
    }
  }
}
