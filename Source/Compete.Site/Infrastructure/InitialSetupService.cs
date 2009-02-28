using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compete.Core.Infrastructure;
using Compete.Model;
using Compete.Model.Repositories;

namespace Compete.Site.Infrastructure
{

  public class InitialSetupService : IInitialSetup
  {
    readonly IConfigurationRepository _configurationRepository;

    public InitialSetupService(IConfigurationRepository configurationRepository)
    {
      _configurationRepository = configurationRepository;
    }

    public bool IsDone
    {
      get { return GetConfiguration() != null; }
    }

    private Configuration GetConfiguration()
    {
      var configuration = HttpContext.Current.Cache["configuration"] as Configuration;

      if (configuration == null)
      {
        configuration = _configurationRepository.GetConfiguration();
      }

      return configuration;
    }
  }
}
