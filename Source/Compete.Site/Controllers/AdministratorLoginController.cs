using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Compete.Site.Infrastructure;

namespace Compete.Site.Controllers
{
  public class AdministratorLoginController : CompeteController
  {
    readonly IAdministratorAuthentication _administratorAuthentication;

    public AdministratorLoginController(IAdministratorAuthentication administratorAuthentication)
    {
      _administratorAuthentication = administratorAuthentication;
    }

    public ActionResult Index(string password)
    {
      if (!String.IsNullOrEmpty(password))
      {
        _administratorAuthentication.Signin(password);
      }
      if (_administratorAuthentication.IsAdministrator)
      {
        return Redirect("~/Administrator");
      }
      return View();
    }
  }
}
