using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Compete.Core.Infrastructure;
using Compete.Model.Game;
using Compete.Site.Infrastructure;
using Compete.Site.Models;

using Microsoft.Practices.ServiceLocation;

namespace Compete.Site.Refereeing
{
  public class Referee
  {
    static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Referee));
    readonly RoundParameters _parameters;

    public Referee(RoundParameters parameters)
    {
      _parameters = parameters;
    }

    public void StartRound()
    {
      using (var staging = new StagingArea(_parameters.AssemblyFiles))
      {
        var sw = new Stopwatch();
        sw.Start();
        var rr = AppDomainHelper.InSeparateAppDomain<RoundParameters, IEnumerable<MatchResult>>(staging.Root, _parameters, RunRound);
        sw.Stop();
        ServiceLocator.Current.GetInstance<IScoreKeeper>().Record(rr);
      }
    }

    private static IEnumerable<MatchResult> RunRound(RoundParameters parameters)
    {
      var competitionFactory = new CompetitionFactory();
      var competition = competitionFactory.CreateCompetition(parameters.AssemblyFiles);
      return competition.PlayRound(parameters.TeamNames);
    }
  }

  [Serializable]
  public class RoundParameters
  {
    public AssemblyFile[] AssemblyFiles
    {
      get;
      private set;
    }

    public string[] TeamNames
    {
      get;
      private set;
    }

    public RoundParameters(AssemblyFile[] files, string[] teamNames)
    {
      AssemblyFiles = files;
      TeamNames = teamNames;
    }

    public static RoundParameters Merge(params RoundParameters[] parameters)
    {
      List<string> teamNames = new List<string>();
      List<AssemblyFile> assemblyFiles = new List<AssemblyFile>();
      foreach (var p in parameters)
      {
        teamNames.AddRange(p.TeamNames);
        assemblyFiles.AddRange(p.AssemblyFiles);
      }
      return new RoundParameters(assemblyFiles.Distinct().ToArray(), teamNames.Distinct().ToArray());
    }
  }
}