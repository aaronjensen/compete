using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Site.Infrastructure;
using Microsoft.Practices.ServiceLocation;

namespace Compete.Site.Filters
{
  public class RequireAuthenticationFilterAttribute : ActionFilterAttribute
  {
    readonly IFormsAuthentication _formsAuthentication;

    public RequireAuthenticationFilterAttribute()
    {
      _formsAuthentication = ServiceLocator.Current.GetInstance<IFormsAuthentication>();
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (!_formsAuthentication.IsCurrentlySignedIn)
      {
        filterContext.Result = new RedirectResult("~/Login");
      }
    }
  }
}
