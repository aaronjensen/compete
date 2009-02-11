using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Model.Repositories;

namespace Compete.Site.Controllers
{
  public class HomeController : Controller
  {
    readonly ITeamRepository _repository;

    public HomeController(ITeamRepository repository)
    {
      _repository = repository;
    }

    public ActionResult Index()
    {
      this.ViewData["teamCount"] = _repository.Count();

      return View();
    }
  }
}