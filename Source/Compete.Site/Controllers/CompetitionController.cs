using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Compete.Site.Filters;

namespace Compete.Site.Controllers
{
  [RequireAuthenticationFilter]
  public class CompetitionController : CompeteController
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}
