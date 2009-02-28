using System;
using Compete.Core.Infrastructure;
using Compete.Model;
using Compete.Model.Repositories;

namespace Compete.Site.Infrastructure
{
  public class AdministratorAuthentication : IAdministratorAuthentication
  {
    readonly IConfigurationRepository _configurationRepository;

    public AdministratorAuthentication(IConfigurationRepository configurationRepository)
    {
      _configurationRepository = configurationRepository;
    }

    public bool IsAdministrator
    {
      get { return true.Equals(System.Web.HttpContext.Current.Session["Administrator"]); }
    }

    public void Signin(string password)
    {
      Configuration configuration = _configurationRepository.GetConfiguration();
      if (configuration == null)
      {
        return;
      }
      System.Web.HttpContext.Current.Session["Administrator"] = configuration.IsAdminPassword(password);
    }
  }
}
