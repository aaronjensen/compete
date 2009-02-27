using System;
using System.IO;
using System.Linq;

using Compete.Bot;
using Compete.Model.Game;
using Compete.Site.Infrastructure;
using Compete.Site.Models;

namespace Compete.Site.Refereeing
{
  public class CompetitionFactory
  {
    public Competition CreateCompetition(AssemblyFile[] files)
    {
      DynamicAssemblyTypeFinder dynamicAssemblyTypeFinder = new DynamicAssemblyTypeFinder();
      dynamicAssemblyTypeFinder.AddAll(files);
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