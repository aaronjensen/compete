using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Core.Infrastructure;
using Compete.Model.Game;
using Compete.Model.Repositories;
using Compete.Site.Filters;
using Compete.Site.Infrastructure;
using Compete.Site.Models;
using Compete.TeamManagement;

namespace Compete.Site.Controllers
{
  [RequireAuthenticationFilter]
  public class MyTeamController : CompeteController
  {
    private readonly IConfigurationRepository _configurationRepository;
    private readonly ITeamManagementQueries _teamManagementQueries;

    public MyTeamController(IConfigurationRepository configurationRepository, ITeamManagementQueries teamManagementQueries)
    {
      _configurationRepository = configurationRepository;
      _teamManagementQueries = teamManagementQueries;
    }

    public ActionResult Index()
    {
      var currentTeam = _teamManagementQueries.GetMyTeamName();
      if (currentTeam == null)
        throw new Exception("Cannot find team " + currentTeam);
      var currentTeamDisplayName = _teamManagementQueries.GetMyTeamDisplayName();

      ViewData["currentTeam"] = currentTeam;
      ViewData["currentTeamDisplayName"] = currentTeamDisplayName;
      ViewData["results"] = _teamManagementQueries.GetMyRecentMatches();
      ViewData["currentRound"] = _configurationRepository.GetConfiguration().RoundNumber;

      return View();
    }
  }
}
