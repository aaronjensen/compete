using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Compete.Site.Infrastructure;

using Microsoft.Practices.ServiceLocation;

namespace Compete.Site.Filters
{
  public class RequireAdministratorPrivilegesFilter : ActionFilterAttribute
  {
    readonly IAdministratorAuthentication _administratorAuthentication;

    public RequireAdministratorPrivilegesFilter()
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
