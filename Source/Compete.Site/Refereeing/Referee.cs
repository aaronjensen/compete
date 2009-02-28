using System;
using System.Collections.Generic;
using System.Diagnostics;

using Compete.Model.Game;
using Compete.Site.Infrastructure;
using Compete.Site.Models;
using System.Linq;

namespace Compete.Site.Refereeing
{
  public class Referee
  {
    static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Referee));
    readonly AssemblyFile[] _files;
    readonly IEnumerable<string> _teamNames;

    public Referee(AssemblyFile[] files, IEnumerable<string> teamNames)
    {
      _files = files;
      _teamNames = teamNames;
    }

    public void StartRound()
    {
      using (var staging = new StagingArea(_files))
      {
        var sw = new Stopwatch();
        sw.Start();
        var rr = AppDomainHelper.InSeparateAppDomain<RoundParameters, IEnumerable<MatchResult>>(staging.Root, new RoundParameters(_files, _teamNames.ToArray()), RunRound);
        sw.Stop();
        Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<IScoreKeeper>().Record(rr);
      }
    }

    [Serializable]
    private class RoundParameters
    {
      public AssemblyFile[] Files 
      {
        get;
        set;
      }

      public IEnumerable<string> TeamNames
      {
        get; set;
      }

      public RoundParameters(AssemblyFile[] files, IEnumerable<string> teamNames)
      {
        Files = files;
        TeamNames = teamNames;
      }
    }

    private static IEnumerable<MatchResult> RunRound(RoundParameters parameters)
    {
      var competitionFactory = new CompetitionFactory();
      var competition = competitionFactory.CreateCompetition(parameters.Files);
      return competition.PlayRound(parameters.TeamNames);
    }
  }
}