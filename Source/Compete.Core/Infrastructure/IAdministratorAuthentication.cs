using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compete.Core.Infrastructure
{
  public interface IAdministratorAuthentication
  {
    bool IsAdministrator { get; }
    void Signin(string password);
  }
}
