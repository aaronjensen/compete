using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Compete.Site.Controllers
{
  public class ViewsController : CompeteController
  {
    public ActionResult SeeView(string viewName)
    {
      ViewData["viewName"] = viewName;
      return View();
    }
  }
}
