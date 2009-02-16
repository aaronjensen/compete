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

    public ActionResult New(string teamName)
    {
      var result = _teamManagementCommands.New(teamName);

      return Redirect("~/");
    }
  }
}
