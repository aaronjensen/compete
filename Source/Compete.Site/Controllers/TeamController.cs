using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Compete.TeamManagement;

namespace Compete.Site.Controllers
{
  public class TeamController : Controller
  {
    private readonly ITeamManagementCommands _teamManagementCommands;

    public TeamController(ITeamManagementCommands teamManagementCommands)
    {
      _teamManagementCommands = teamManagementCommands;
    }

    public ActionResult New(string teamName, string longName, string teamMember)
    {
      var teamMembers = teamMember.Split(',').Where(x=>!x.Equals(string.Empty));

      var result = _teamManagementCommands.New(teamName, longName, teamMembers);

      return Redirect("~/");
    }
  }
}
