using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Model.Repositories;
using Compete.TeamManagement;

namespace Compete.Site.Controllers
{
  public class HomeController : Controller
  {
    readonly ITeamRepository _repository;
    private readonly ITeamManagementQueries _teamManagementQueries;

    public HomeController(ITeamRepository repository, ITeamManagementQueries teamManagementQueries)
    {
      _repository = repository;
      _teamManagementQueries = teamManagementQueries;
    }

    public ActionResult Index()
    {
      this.ViewData["teamSummaries"] = _teamManagementQueries.GetTeamSummaries();
      ViewData["teamCount"] = _repository.Count();

      return View();
    }

    public ActionResult TeamSignup()
    {
      ViewData["teamCount"] = _repository.Count();
      return View();
    }
  }
}