using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Model.Repositories;
using Compete.Site.Filters;

namespace Compete.Site.Controllers
{
  [RequireAdministratorPrivilegesFilter]
  public class ConfigurationController : CompeteController
  {
    readonly IConfigurationRepository _configurationRepository;

    public ConfigurationController(IConfigurationRepository configurationRepository)
    {
      _configurationRepository = configurationRepository;
    }

    public ActionResult EnableUploads()
    {
      var configuration = _configurationRepository.GetConfiguration();
      configuration.EnableUploads();

      _configurationRepository.SetConfiguration(configuration);

      return Redirect("~/Administrator");
    }

    public ActionResult DisableUploads()
    {
      var configuration = _configurationRepository.GetConfiguration();
      configuration.DisableUploads();

      _configurationRepository.SetConfiguration(configuration);

      return Redirect("~/Administrator");
    }
  }
}
