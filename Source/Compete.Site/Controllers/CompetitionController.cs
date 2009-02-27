using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
      RoundResult rr = AppDomainHelper.InSeparateAppDomain<RoundResult>(RunRound);
      return View();
    }

    private static RoundResult RunRound()
    {
      CompetitionFactory competitionFactory = new CompetitionFactory(new AssemblyFileRepository());
      Competition competition = competitionFactory.CreateCompetition();
      competition.PlayRound();
      return null;
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
        competition.AddPlayer(new BotPlayer("", botFactory.CreateBot()));
      }
      return competition;
    }
  }
}
