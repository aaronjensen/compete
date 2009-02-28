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

    public void EnableUploads()
    {
      UploadsAreEnabled = true;
    }

    public void DisableUploads()
    {
      UploadsAreEnabled = false;
    }

    public bool UploadsAreEnabled
    {
      get; private set;
    }

    public object IsAdminPassword(string password)
    {
      return AdminPassword.Equals(password);
    }
  }
}
