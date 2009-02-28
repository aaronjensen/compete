using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Site.Infrastructure;
using Compete.TeamManagement;

namespace Compete.Site.Controllers
{
  public class CreateTeamController : CompeteController
  {
    readonly ITeamManagementCommands _teamManagementCommands;
    private readonly ITeamManagementQueries _teamManagementQueries;
    readonly ISignin _signin;

    public CreateTeamController(ITeamManagementCommands teamManagementCommands, ITeamManagementQueries teamManagementQueries, ISignin signin)
    {
      _teamManagementCommands = teamManagementCommands;
      _teamManagementQueries = teamManagementQueries;
      _signin = signin;
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index()
    {
      if (_teamManagementQueries.IsSignedIn)
      {
        return Redirect(@"~/MyTeam");
      }

      this.ViewData["ErrorMessage"] = "none";
      return View();
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Index(FormCollection form)
    {
      var teamMember = form["teamMember"];
      var teamName = form["teamName"];
      var dispalyName = form["displayName"];
      var password = form["password"];
      var passwordAgain = form["passwordAgain"];

      if (!passwordAgain.Equals(password))
      {
        this.ViewData["ErrorMessage"] = "Passwords do not match";
        return View();
      }
      
      if (teamName.Equals(string.Empty))
      {
        this.ViewData["ErrorMessage"] = "You cannot create a team with an empty team name. Try again.";
        return View();
      }

      if (!_teamManagementQueries.TeamNameIsAvailable(teamName))
      {
        this.ViewData["ErrorMessage"] = "Team name '" + teamName + "' is already taken, sorry.";
        return View();
      }

      var teamMembers = teamMember.Split(',').Where(x=>!x.Equals(string.Empty));

      var result = _teamManagementCommands.New(teamName, dispalyName, teamMembers, password);

      if (!result)
      {
        throw new Exception("Crazy wild error creating a team");
      }
      
      result = _signin.Signin(teamName, password);
      if (!result)
      {
        throw new Exception("Weird, I seriously just added this team, I should be able to log you in...");
      }

      return Redirect("~/MyTeam");
    }
  }
}
