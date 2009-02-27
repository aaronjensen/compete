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

namespace Compete.Site.Controllers
{
  [RequireAuthenticationFilter]
  public class CompetitionController : CompeteController
  {
    public ActionResult Index()
    {
      RoundResult rr = AppDomainHelper.InSeparateAppDomain<object, RoundResult>(null, RunRound);
      _log.Info(rr);
      return View();
    }

    private static RoundResult RunRound(object notUsed)
    {
      CompetitionFactory competitionFactory = new CompetitionFactory(new AssemblyFileRepository());
      Competition competition = competitionFactory.CreateCompetition();
      return competition.PlayRound();
    }
  }

  public class CompetitionFactory
  {
    readonly AssemblyFileRepository _assemblyFileRepository;

    public CompetitionFactory(AssemblyFileRepository assemblyFileRepository)
    {
      _assemblyFileRepository = assemblyFileRepository;
    }

    public Competition CreateCompetition()
    {
      DynamicAssemblyTypeFinder dynamicAssemblyTypeFinder = new DynamicAssemblyTypeFinder();
      dynamicAssemblyTypeFinder.AddAll(_assemblyFileRepository.FindAllGames());
      dynamicAssemblyTypeFinder.AddAll(_assemblyFileRepository.FindAllPlayers());
      IGame game = dynamicAssemblyTypeFinder.Create<IGame>().Single();
      Competition competition = new Competition(game);
      foreach (IBotFactory botFactory in dynamicAssemblyTypeFinder.Create<IBotFactory>())
      {
        string teamName = Path.GetFileName(botFactory.GetType().Assembly.Location);
        competition.AddPlayer(new BotPlayer(teamName, botFactory.CreateBot()));
      }
      return competition;
    }
  }
}
