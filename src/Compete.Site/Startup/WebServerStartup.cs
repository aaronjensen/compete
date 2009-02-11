using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Compete.Site.Startup
{
  public class WebServerStartup
  {
    readonly IControllerFactory _controllerFactory;

    public WebServerStartup(IControllerFactory controllerFactory)
    {
      _controllerFactory = controllerFactory;
    }

    public void Start()
    {
      ControllerBuilder.Current.SetControllerFactory(_controllerFactory);
    }
  }
}
