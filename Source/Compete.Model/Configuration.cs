using System;
using System.Collections.Generic;

namespace Compete.Model
{
  public class Configuration : Entity
  {
    public Configuration()
      : base(Guid.Empty)
    {
    }

    public string AdminPassword
    {
      set; private get;
    }

    public object IsAdminPassword(string password)
    {
      return AdminPassword.Equals(password);
    }
  }
}
