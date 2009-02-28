using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using Compete.Core.Infrastructure;

namespace Compete.Site.Infrastructure
{

  public class FormsAuthenticationService : IFormsAuthentication
  {
    public void SignIn(string userName)
    {
      FormsAuthentication.SetAuthCookie(userName, true);
    }

    public void SignOut()
    {
      HttpContext.Current.Session.Abandon();
      FormsAuthentication.SignOut();
    }

    public bool IsCurrentlySignedIn
    {
      get { return HttpContext.Current.User.Identity.IsAuthenticated; }
    }

    public string SignedInUserName
    {
      get { return HttpContext.Current.User.Identity.Name; }
    }
  }
}
