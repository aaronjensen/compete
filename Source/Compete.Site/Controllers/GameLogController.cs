using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Site.Infrastructure;

namespace Compete.Site.Controllers
{
  public class GameLogController : CompeteController
  {
    readonly IAdministratorAuthentication _administratorAuthentication;
    readonly IFormsAuthentication _formsAuthentication;

    public GameLogController(IAdministratorAuthentication administratorAuthentication, IFormsAuthentication formsAuthentication)
    {
      _administratorAuthentication = administratorAuthentication;
      _formsAuthentication = formsAuthentication;
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

      ViewData["teamName"] = teamName;
      ViewData["otherTeamName"] = otherTeamName;
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
