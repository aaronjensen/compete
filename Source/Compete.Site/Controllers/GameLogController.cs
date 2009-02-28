using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Core.Infrastructure;
using Compete.Model.Repositories;
using Compete.Site.Infrastructure;

namespace Compete.Site.Controllers
{
  public class GameLogController : CompeteController
  {
    readonly IAdministratorAuthentication _administratorAuthentication;
    readonly IFormsAuthentication _formsAuthentication;
    readonly ILeaderboardRepository _leaderboardRepository;

    public GameLogController(IAdministratorAuthentication administratorAuthentication, IFormsAuthentication formsAuthentication, ILeaderboardRepository leaderboardRepository)
    {
      _administratorAuthentication = administratorAuthentication;
      _formsAuthentication = formsAuthentication;
      _leaderboardRepository = leaderboardRepository;
    }

    public ActionResult Index(string teamName, string otherTeamName)
    {
      if (String.IsNullOrEmpty(teamName) || string.IsNullOrEmpty(otherTeamName))
      {
        return Redirect("~/MyTeam");
      }

      if (!CanSeeLogForMatch(teamName, otherTeamName))
      {
        return View("Nope");
      }

      var leaderboard = _leaderboardRepository.GetLeaderboard();
      var result = leaderboard.GetMatchResultsForMatchBetween(teamName, otherTeamName);

      if (result == null)
        return View("NoMatch");

      ViewData["result"] = result;
      ViewData["log"] = result.Log;
      return View();
    }

    bool CanSeeLogForMatch(string teamName, string otherTeamName)
    {
      if (_administratorAuthentication.IsAdministrator)
        return true;

      if (_formsAuthentication.SignedInUserName == teamName
        || _formsAuthentication.SignedInUserName == otherTeamName)
        return true;

      return false;
    }
  }
}
