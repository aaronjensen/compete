using System;
using System.Diagnostics;

using Compete.Model.Game;
using Compete.Site.Infrastructure;
using Compete.Site.Models;

namespace Compete.Site.Refereeing
{
  public class Referee
  {
    static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Referee));
    readonly AssemblyFile[] _files;

    public Referee(AssemblyFile[] files)
    {
      _files = files;
    }

    public void Start()
    {
      using (StagingArea staging = new StagingArea(_files))
      {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        RoundResult rr = AppDomainHelper.InSeparateAppDomain<AssemblyFile[], RoundResult>(staging.Root, _files, RunRound);
        sw.Stop();
        _log.Info("RR: " + rr + " completed in " + sw.Elapsed);
      }
    }

    private static RoundResult RunRound(AssemblyFile[] files)
    {
      CompetitionFactory competitionFactory = new CompetitionFactory();
      Competition competition = competitionFactory.CreateCompetition(files);
      return competition.PlayRound();
    }
  }
}
