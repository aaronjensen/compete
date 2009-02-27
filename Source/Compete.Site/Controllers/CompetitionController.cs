using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

using Compete.Bot;
using Compete.Model.Game;
using Compete.Site.Filters;
using Compete.Site.Infrastructure;
using Compete.Site.Models;
using Compete.Site.Refereeing;

namespace Compete.Site.Controllers
{
  [RequireAuthenticationFilter]
  public class CompetitionController : CompeteController
  {
    readonly AssemblyFileRepository _assemblyFileRepository = new AssemblyFileRepository();

    public ActionResult Index()
    {
      Referee referee = new Referee(_assemblyFileRepository.FindAllGamesAndPlayers().ToArray());
      RefereeThread thread = new RefereeThread(referee);
      thread.Start();
      return View();
    }
  }
}
