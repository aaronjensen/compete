using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Site.Infrastructure;
using Compete.TeamManagement;

namespace Compete.Site.Controllers
{
  public class LoginController : CompeteController
  {
    readonly ITeamManagementCommands _teamCommands;
    readonly IFormsAuthentication _formsAuthentication;

    public LoginController(ITeamManagementCommands teamCommands, IFormsAuthentication formsAuthentication)
    {
      _teamCommands = teamCommands;
      _formsAuthentication = formsAuthentication;
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index()
    {
      return View();
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Index(string teamName, string password, string returnUrl)
    {
      var result = _teamCommands.Authenticate(teamName, password);

      if (result)
      {
        _formsAuthentication.SignIn(teamName);
        if (String.IsNullOrEmpty(returnUrl))
        {
          return Redirect("~/MyTeam");
        }

        return Redirect(returnUrl);
      }
      else
      {
        ViewData["error"] = "Invalid Team Name or Password";
        return View();
      }
    }
  }
}
