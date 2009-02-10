using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Compete.Core;
using Compete.Site.Startup;
using Machine.Container;
using Machine.Container.Plugins;
using Machine.Container.Services;
using Machine.MsMvc;
using Spark.Web.Mvc;

namespace Compete.Site
{
  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801

  public class MvcApplication : System.Web.HttpApplication
  {
    private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(MvcApplication));
    IMachineContainer _container;

    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          "Default",                                              // Route name
          "{controller}/{action}/{id}",                           // URL with parameters
          new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
      );

    }

    protected void Application_Start()
    {
      log4net.Config.XmlConfigurator.Configure();

      _container = CreateContainer();
      _container.Resolve.Object<WebServerStartup>().Start();

      ViewEngines.Engines.Add(new SparkViewFactory());

      RegisterRoutes(RouteTable.Routes);
    }

    protected static IMachineContainer CreateContainer()
    {
      IMachineContainer container = new MachineContainer();
      ContainerRegistrationHelper helper = new ContainerRegistrationHelper(container);
      container.Initialize();
      container.PrepareForServices();
      helper.AddServiceCollection(new CoreServices());
      helper.AddServiceCollection(new SiteServices());
      helper.AddServiceCollection(new MsMvcServices());
      container.Start();
      _log.Info("Container Ready");
      return container;
    }
  }

  public class SiteServices : IServiceCollection
  {
    public void RegisterServices(ContainerRegisterer register)
    {
      register.Type<WebServerStartup>();
    }
  }
}