using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Compete.Model;
using Compete.Model.Repositories;

namespace Compete.TeamManagement
{
  public interface ITeamManagementCommands
  {
    bool New(string teamName, string longName, IEnumerable<string> teamMembers, string password);
    bool Authenticate(string teamName, string password);
  }

  public class TeamManagementCommands : ITeamManagementCommands
  {
    private readonly ITeamRepository _repository;

    public TeamManagementCommands(ITeamRepository teamRepository)
    {
      _repository = teamRepository;
    }

    string GetMd5HashOf(string password)
    {
      // lifted from http://blog.stevex.net/index.php/c-code-snippet-creating-an-md5-hash-string/

      // First we need to convert the string into bytes, which
      // means using a text encoder.
      Encoder enc = System.Text.Encoding.Unicode.GetEncoder();

      // Create a buffer large enough to hold the string
      byte[] unicodeText = new byte[password.Length * 2];
      enc.GetBytes(password.ToCharArray(), 0, password.Length, unicodeText, 0, true);

      // Now that we have a byte array we can ask the CSP to hash it
      MD5 md5 = new MD5CryptoServiceProvider();
      byte[] result = md5.ComputeHash(unicodeText);

      // Build the final string by converting each byte
      // into hex and appending it to a StringBuilder
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < result.Length; i++)
      {
        sb.Append(result[i].ToString("X2"));
      }

      // And return it
      return sb.ToString();

    }

    public bool New(string teamName, string longName, IEnumerable<string> teamMembers, string password)
    {
      var members = teamMembers.Select(x => new TeamMember(x));
      Team team;
      try
      {
        team = new Team(teamName, longName, members, GetMd5HashOf(password));
      }
      catch (ArgumentException e)
      {
        return false;
      }
      _repository.Add(team);
      return true;
    }

    public bool Authenticate(string teamName, string password)
    {
      var team = _repository.FindByTeamName(teamName);

      if (team == null)
      {
        return false;
      }

      var result = team.Authenticate(GetMd5HashOf(password));

      return result;
    }
  }
}