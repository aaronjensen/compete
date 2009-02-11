using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Model;
using Compete.Model.Repositories;

namespace Compete.Site.Controllers
{
  public class TeamController : Controller
  {
    readonly ITeamRepository _repository;

    public TeamController(ITeamRepository repository)
    {
      _repository = repository;
    }

    public ActionResult New(string teamName)
    {
      var team = new Team(teamName);
      _repository.Add(team);

      return Redirect("~/");
    }
  }
}
