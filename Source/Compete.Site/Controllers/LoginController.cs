using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Compete.Site.Controllers
{
  public class LoginController : Controller
  {
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index()
    {
      return View();
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Index(string teamName, string password)
    {
      return View();
    }
  }
}
