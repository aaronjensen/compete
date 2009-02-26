using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.TeamManagement;

namespace Compete.Site.Controllers
{
  public class CreateTeamController : Controller
  {
    readonly ITeamManagementCommands _teamManagementCommands;

    public CreateTeamController(ITeamManagementCommands teamManagementCommands)
    {
      _teamManagementCommands = teamManagementCommands;
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index()
    {
      return View();
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Index(string teamName, string longName, string teamMember, string password)
    {
      var teamMembers = teamMember.Split(',').Where(x=>!x.Equals(string.Empty));

      var result = _teamManagementCommands.New(teamName, longName, teamMembers, password);

      if (!result)
      {
        throw new Exception("Crazy wild error creating a team");
      }
      
      result = _teamManagementCommands.Authenticate(teamName, password);
      if (!result)
      {
        throw new Exception("Weird, I seriously just added this team, I should be able to log you in...");
      }

      return Redirect("~/MyTeam");
    }
  }
}
