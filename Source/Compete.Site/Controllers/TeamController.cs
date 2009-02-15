using System.Web.Mvc;
using Compete.TeamManagement;

namespace Compete.Site.Controllers
{
  public class TeamController : Controller
  {
    private readonly ITeamManagementService _teamManagementService;

    public TeamController(ITeamManagementService teamManagementService)
    {
      _teamManagementService = teamManagementService;
    }

    public ActionResult New(string teamName)
    {
      var result = _teamManagementService.New(teamName);

      return Redirect("~/");
    }
  }
}
