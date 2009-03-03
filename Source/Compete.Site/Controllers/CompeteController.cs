using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Compete.Site.Filters;
using Spark.Web.Mvc;

namespace Compete.Site.Controllers
{
  [RequireInitialSetupFilter]
  [ProvideRefereeStatusFilter]
  public class CompeteController : Controller
  {
    protected readonly log4net.ILog _log;

    public CompeteController()
    {
      _log = log4net.LogManager.GetLogger(GetType());
    }

    public ActionResult RedirectToReferrer(Func<ActionResult> noReferrer)
    {
      if (this.Request.UrlReferrer == null)
      {
        return noReferrer();
      }
      return Redirect(this.Request.UrlReferrer.AbsoluteUri);
    }

    public ActionResult RedirectToReferrer()
    {
      return RedirectToReferrer(() => RedirectToAction("Index", "Home"));
    }

    public virtual ActionResult GetView(string viewName)
    {
      return new JavascriptViewResult { ViewName = viewName };
    }
  }
}
