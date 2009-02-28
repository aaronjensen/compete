using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compete.Core.Infrastructure
{
  public interface IFormsAuthentication
  {
    void SignIn(string userName);
    void SignOut();
    bool IsCurrentlySignedIn { get; }
    string SignedInUserName { get; }
  }
}
