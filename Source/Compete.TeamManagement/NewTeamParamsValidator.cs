using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compete.TeamManagement
{
  public interface INewTeamParamsValidator
  {
    bool PasswordsMatch(string password, string passwordAgain);
    bool TeamNameIsNotEmpty(string name);
    bool TeamNameIsAvailable(string name);
  }

  public class NewTeamParamsValidator : INewTeamParamsValidator
  {
    private readonly ITeamManagementQueries _teamManagementQueries;

    public NewTeamParamsValidator(ITeamManagementQueries teamManagementQueries)
    {
      _teamManagementQueries = teamManagementQueries;
    }

    public bool PasswordsMatch(string password, string passwordAgain)
    {
      return passwordAgain.Equals(password);
    }

    public bool TeamNameIsNotEmpty(string name)
    {
      return !name.Equals(string.Empty);
    }

    public bool TeamNameIsAvailable(string name)
    {
      return _teamManagementQueries.TeamNameIsAvailable(name);
    }
  }
}