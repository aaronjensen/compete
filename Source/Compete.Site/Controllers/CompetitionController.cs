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
    readonly CompetitionFactory _competitionFactory = new CompetitionFactory(new AssemblyFileRepository());

    public ActionResult Index()
    {
      Competition competition = _competitionFactory.CreateCompetition();
      competition.PlayRound();
      return View();
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
      foreach (IPlayerFactory playerFactory in dynamicAssemblyTypeFinder.Create<IPlayerFactory>())
      {
        competition.AddPlayer(playerFactory.CreatePlayer());
      }
      return competition;
    }
  }
}
