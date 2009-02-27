using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Compete.Site.Infrastructure;

namespace Compete.Site.Controllers
{
  public class LoginController : CompeteController
  {
    readonly ISignin _signin;

    public LoginController(ISignin signin)
    {
      _signin = signin;
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index()
    {
      return View();
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Index(string teamName, string password, string returnUrl)
    {
      var result = _signin.Signin(teamName, password);

      if (result)
      {
        if (String.IsNullOrEmpty(returnUrl))
        {
          return Redirect("~/MyTeam");
        }

        return Redirect(returnUrl);
      }
      else
      {
        ViewData["error"] = "Invalid Team Name or Password";
        return View();
      }
    }
  }
}
