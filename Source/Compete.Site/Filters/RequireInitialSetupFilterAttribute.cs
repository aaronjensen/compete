using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Site.Infrastructure;
using Microsoft.Practices.ServiceLocation;

namespace Compete.Site.Filters
{
  public class RequireInitialSetupFilterAttribute : ActionFilterAttribute
  {
    readonly IInitialSetup _setup;

    public RequireInitialSetupFilterAttribute()
    {
      _setup = ServiceLocator.Current.GetInstance<IInitialSetup>();
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (!_setup.IsDone)
      {
        filterContext.Result = new RedirectResult("~/InitialSetup");
      }
    }
  }
}
