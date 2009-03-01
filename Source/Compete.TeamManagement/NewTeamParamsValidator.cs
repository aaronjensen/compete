using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Compete.TeamManagement
{
  public interface INewTeamParamsValidator
  {
    bool PasswordsMatch(string password, string passwordAgain);
    bool TeamNameIsValid(string name);
    bool TeamNameIsAvailable(string name);
  }

  public class NewTeamParamsValidator : INewTeamParamsValidator
  {
    private readonly ITeamManagementQueries _teamManagementQueries;
    readonly Regex nonWordCharacters = new Regex(@"\W+");

    public NewTeamParamsValidator(ITeamManagementQueries teamManagementQueries)
    {
      _teamManagementQueries = teamManagementQueries;
    }

    public bool PasswordsMatch(string password, string passwordAgain)
    {
      return passwordAgain.Equals(password);
    }

    public bool TeamNameIsValid(string name)
    {
      if (nonWordCharacters.IsMatch(name))
        return false;

      if (name.Length > 16)
        return false;

      if (name.Equals(string.Empty))
        return false;

      return true;
    }

    public bool TeamNameIsAvailable(string name)
    {
      return _teamManagementQueries.TeamNameIsAvailable(name);
    }
  }
}