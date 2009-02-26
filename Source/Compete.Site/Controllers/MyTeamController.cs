using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compete.Site.Filters;

namespace Compete.Site.Controllers
{
  [RequireAuthenticationFilter]
  public class MyTeamController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}
