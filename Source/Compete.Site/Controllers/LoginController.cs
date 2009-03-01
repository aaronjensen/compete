using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Compete.Site.Infrastructure;
using Compete.TeamManagement;

namespace Compete.Site.Controllers
{
  public class LoginController : CompeteController
  {
    readonly ISignin _signin;
    private readonly ITeamManagementQueries _teamManagementQueries;

    public LoginController(ISignin signin, ITeamManagementQueries teamManagementQueries)
    {
      _signin = signin;
      _teamManagementQueries = teamManagementQueries;
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index()
    {
      if (_teamManagementQueries.IsSignedIn)
      {
        return Redirect(@"~/MyTeam");
      }

      return View();
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Index(string teamName, string password, string returnUrl)
    {
      var result = _signin.Signin(teamName, password);

      if (result)
      {
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
