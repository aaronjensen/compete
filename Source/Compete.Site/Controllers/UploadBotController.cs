using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Compete.Model;
using Compete.Core.Infrastructure;
using Compete.Model;

using Compete.Model.Repositories;
using Compete.Site.Infrastructure;
using Compete.Site.Models;
using Compete.TeamManagement;

namespace Compete.Site.Controllers
{
  public class UploadBotController : CompeteController
  {
    readonly ITeamManagementCommands _teamCommands;
    readonly IFormsAuthentication _formsAuthentication;
    readonly IConfigurationRepository _configurationRepository;
    readonly AssemblyFileRepository _assemblyFileRepository = new AssemblyFileRepository();
    readonly MatchStarter _matchStarter;

    public UploadBotController(ITeamManagementCommands teamCommands, IFormsAuthentication formsAuthentication, IConfigurationRepository configurationRepository, MatchStarter matchStarter)
    {
      _teamCommands = teamCommands;
      _formsAuthentication = formsAuthentication;
      _configurationRepository = configurationRepository;
      _matchStarter = matchStarter;
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Index(string teamName, string password)
    {
      var configuration = _configurationRepository.GetConfiguration();

      if (!configuration.UploadsAreEnabled)
      {
        Response.Write("Uploads are currently disabled");
        Response.StatusCode = (int)HttpStatusCode.Forbidden;
        return new EmptyResult();
      }

      if (string.IsNullOrEmpty(teamName) || string.IsNullOrEmpty(password))
      {
        teamName = _formsAuthentication.SignedInUserName;
      }
      else
      {
        if (!_teamCommands.Authenticate(teamName, password))
        {
          teamName = null;
        }
      }

      if (string.IsNullOrEmpty(teamName))
      {
        Response.StatusCode = (int)HttpStatusCode.Forbidden;
        Response.Write("Invalid teamname or password.");
        return new EmptyResult();
      }

      if (Request.Files.Count != 1)
      {
        throw new FileLoadException("only one file at a time, please. you loaded " + Request.Files.Count + ".");
      }

      foreach (string file in Request.Files)
      {
        var hpf = Request.Files[file];

        if (hpf.ContentLength == 0)
        {
          continue;
        }
        if (!hpf.FileName.Split('.').Last().ToLower().Equals("dll"))
        {
          throw new ArgumentException("only .dll files only, please");
        }
        _assemblyFileRepository.Add(hpf, teamName + ".dll");
        // _matchStarter.Queue(teamName);
      }
      return Redirect("~/MyTeam");
    }
  }
}
