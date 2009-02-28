using System;
using System.Web.Mvc;
using Compete.Model.Repositories;
using Compete.Site.Filters;

namespace Compete.Site.Controllers
{
  [RequireAdministratorPrivilegesFilter]
  public class AdministratorController : CompeteController
  {
    readonly IConfigurationRepository _configurationRepository;

    public AdministratorController(IConfigurationRepository configurationRepository)
    {
      _configurationRepository = configurationRepository;
    }

    public ActionResult Index()
    {
      ViewData["configuration"] = _configurationRepository.GetConfiguration();

      return View();
    }
  }
}
