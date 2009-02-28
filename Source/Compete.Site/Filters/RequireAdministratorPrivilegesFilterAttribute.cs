using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Compete.Core.Infrastructure;
using Compete.Site.Infrastructure;

using Microsoft.Practices.ServiceLocation;

namespace Compete.Site.Filters
{
  public class RequireAdministratorPrivilegesFilterAttribute : ActionFilterAttribute
  {
    readonly IAdministratorAuthentication _administratorAuthentication;

    public RequireAdministratorPrivilegesFilterAttribute()
    {
      _administratorAuthentication = ServiceLocator.Current.GetInstance<IAdministratorAuthentication>();
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (!_administratorAuthentication.IsAdministrator)
      {
        filterContext.Result = new RedirectResult("~/AdministratorLogin");
      }
    }
  }
}
