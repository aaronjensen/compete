﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Site.Infrastructure;

namespace Compete.Site.Controllers
{
  public class LogoutController : Controller
  {
    readonly IFormsAuthentication _authentication;

    public LogoutController(IFormsAuthentication authentication)
    {
      _authentication = authentication;
    }

    public ActionResult Index()
    {
      _authentication.SignOut();

      return Redirect("~/");
    }
  }
}