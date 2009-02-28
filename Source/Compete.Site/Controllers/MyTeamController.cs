using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    readonly ITeamRepository _teamRepository;
    readonly ILeaderboardRepository _leaderboardRepository;
    readonly IFormsAuthentication _formsAuthentication;

    public MyTeamController(ITeamRepository teamRepository, ILeaderboardRepository leaderboardRepository, IFormsAuthentication formsAuthentication)
    {
      _teamRepository = teamRepository;
      _leaderboardRepository = leaderboardRepository;
      _formsAuthentication = formsAuthentication;
    }

    public ActionResult Index()
    {
      var currentTeam = _teamRepository.FindByTeamName(_formsAuthentication.SignedInUserName);
      if (currentTeam == null)
        throw new Exception("Cannot find team " + _formsAuthentication.SignedInUserName);

      var leaderboard = _leaderboardRepository.GetLeaderboard();
      if (leaderboard == null)
        throw new Exception("Huh, where's the leaderboard?");

      var results = leaderboard.GetMatchResultsForTeam(currentTeam.Name);

      ViewData["currentTeam"] = currentTeam;
      ViewData["results"] = results.Select(x => new RecentMatch(currentTeam.Name, x));

      return View();
    }
  }
}
