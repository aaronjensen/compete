using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

using Compete.Core;
using Compete.Persistence;
using Compete.Site.Infrastructure;
using Compete.Site.Startup;
using Compete.TeamManagement;

using Machine.Container;
using Machine.Container.Services;
using Machine.MsMvc;

using Microsoft.Practices.ServiceLocation;

using Spark.Web.Mvc;

namespace Compete.Site
{
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
      routes.MapRoute(
          "TeamSignup",                                              // Route name
          "TeamSignup",                           // URL with parameters
          new { controller = "Home", action = "TeamSignup"}  // Parameter defaults
      );

    }

    protected void Application_Start()
    {
      log4net.Config.XmlConfigurator.Configure();

      var path = Path.GetDirectoryName(Server.MapPath("~/Web.config"));

      AppDomainHelper.Start(path);
      Database.Start(path);
      
      _container = CreateContainer();
      _container.Resolve.Object<WebServerStartup>().Start();

      ViewEngines.Engines.Add(new SparkViewFactory());

      RegisterRoutes(RouteTable.Routes);
    }

    protected static IMachineContainer CreateContainer()
    {
      var container = new MachineContainer();
      ContainerRegistrationHelper helper = new ContainerRegistrationHelper(container);
      container.Initialize();
      container.PrepareForServices();
      helper.AddServiceCollection(new CoreServices());
      helper.AddServiceCollection(new SiteServices());
      helper.AddServiceCollection(new PersistenceServices());
      helper.AddServiceCollection(new MsMvcServices());
      helper.AddServiceCollection(new TeamManagementServices());
      container.Start();
      _log.Info("Container Ready");

      IoC.Container = container;
      var adapter = new CommonServiceLocatorAdapter(container);
      ServiceLocator.SetLocatorProvider(() => adapter);
      return container;
    }
  }
}