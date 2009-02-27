using System.Linq;
using System.Web.Mvc;
using Compete.Site.Infrastructure;
using Compete.Site.Startup;
using Machine.Container;
using Machine.Container.Plugins;
using Machine.Core;

namespace Compete.Site
{
  public class SiteServices : IServiceCollection
  {
    public void RegisterServices(ContainerRegisterer register)
    {
      register.Type<WebServerStartup>();
      register.Type<IFormsAuthentication>().ImplementedBy<FormsAuthenticationService>();
      register.Type<ISignin>().ImplementedBy<SigninService>();
      register.Type<IInitialSetup>().ImplementedBy<InitialSetupService>();

      GetType().Assembly.GetExportedTypes().Where(x => typeof(Controller).IsAssignableFrom(x)).Each(
        x => register.Type(x).AsTransient()
        );
    }
  }
}