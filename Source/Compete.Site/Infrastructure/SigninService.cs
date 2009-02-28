using System;
using System.Collections.Generic;
using Compete.Core.Infrastructure;
using Compete.TeamManagement;

namespace Compete.Site.Infrastructure
{
  public interface ISignin
  {
    bool Signin(string username, string password);
  }

  public class SigninService : ISignin
  {
    readonly ITeamManagementCommands _teamCommands;
    readonly IFormsAuthentication _formsAuthentication;

    public SigninService(ITeamManagementCommands teamCommands, IFormsAuthentication formsAuthentication)
    {
      _teamCommands = teamCommands;
      _formsAuthentication = formsAuthentication;
    }

    public bool Signin(string teamName, string password)
    {
      var result = _teamCommands.Authenticate(teamName, password);

      if (result)
      {
        _formsAuthentication.SignIn(teamName);
      }

      return result;
    }
  }
}
