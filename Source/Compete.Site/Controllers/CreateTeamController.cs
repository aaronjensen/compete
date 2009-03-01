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
    private readonly INewTeamParamsValidator _newTeamParamsValidator;

    public CreateTeamController(ITeamManagementCommands teamManagementCommands, ITeamManagementQueries teamManagementQueries, ISignin signin, INewTeamParamsValidator newTeamParamsValidator)
    {
      _teamManagementCommands = teamManagementCommands;
      _teamManagementQueries = teamManagementQueries;
      _signin = signin;
      _newTeamParamsValidator = newTeamParamsValidator;
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index()
    {
      if (_teamManagementQueries.IsSignedIn)
      {
        return Redirect(@"~/MyTeam");
      }

      this.ViewData["ErrorMessage"] = string.Empty;
      return View();
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Index(FormCollection form)
    {
      var teamMembers = form["teamMember"].Split(',').Where(x=>!x.Equals(string.Empty));;
      var teamName = form["teamName"];
      var displayName = form["displayName"];
      var password = form["password"];
      var passwordAgain = form["passwordAgain"];

      return CreateTeam(teamName, displayName, password, passwordAgain, teamMembers);
    }

    ActionResult CreateTeam(string teamName, string displayName, string password, string passwordAgain, IEnumerable<string> teamMembers)
    {
      var passwordIsValid = _newTeamParamsValidator.PasswordsMatch(password, passwordAgain);
      var teamNameIsNotEmpty = _newTeamParamsValidator.TeamNameIsValid(teamName);
      var teamNameIsAvailable = _newTeamParamsValidator.TeamNameIsAvailable(teamName);

      var errors = string.Empty;
      errors += passwordIsValid ? string.Empty : "Passwords do not match. ";
      errors += teamNameIsNotEmpty ? string.Empty : "You cannot create a team with an empty team name. Try again. ";
      errors += teamNameIsAvailable ? string.Empty :  "Team name '" + teamName + "' is already taken, sorry. ";

      ViewData["ErrorMessage"] = errors;
      if (!errors.Equals(string.Empty))
        return View();

      if (!_teamManagementCommands.New(teamName, displayName, teamMembers, password))
        throw new Exception("Crazy wild error creating a team");
      
      if (!_signin.Signin(teamName, password))
        throw new Exception("Weird, I seriously just added this team, I should be able to log you in...");

      return Redirect("~/MyTeam");
    }
  }
}
