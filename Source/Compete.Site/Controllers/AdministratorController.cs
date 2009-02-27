using System;
using System.Web.Mvc;

using Compete.Site.Filters;

namespace Compete.Site.Controllers
{
  [RequireAdministratorPrivilegesFilter]
  public class AdministratorController : CompeteController
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}
