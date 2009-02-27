using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Model;
using Compete.Model.Repositories;

namespace Compete.Site.Controllers
{
  public class InitialSetupController : Controller
  {
    readonly IConfigurationRepository _configurationRepository;

    public InitialSetupController(IConfigurationRepository configurationRepository)
    {
      _configurationRepository = configurationRepository;
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index()
    {
      return View();
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Index(string password, string confirmPassword)
    {
      if (String.IsNullOrEmpty(password))
      {
        ViewData["error"] = "Enter an admin password";
        return View();
      }
      
      if (password != confirmPassword)
      {
        ViewData["error"] = "Passwords didn't match";
        return View();
      }

      var configuration = new Configuration {AdminPassword = password};
      _configurationRepository.SetConfiguration(configuration);

      return Redirect("~/");
    }
  }
}
