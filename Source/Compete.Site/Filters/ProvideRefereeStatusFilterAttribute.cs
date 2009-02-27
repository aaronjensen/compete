using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Compete.Site.Refereeing;

using Microsoft.Practices.ServiceLocation;

namespace Compete.Site.Filters
{
  public class ProvideRefereeStatusFilterAttribute : ActionFilterAttribute
  {
    readonly IRefereeThread _refereeThread;

    public ProvideRefereeStatusFilterAttribute()
    {
      _refereeThread = ServiceLocator.Current.GetInstance<IRefereeThread>();
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      filterContext.Controller.ViewData["IsRunning"] = _refereeThread.IsRunning;
    }
  }
}

