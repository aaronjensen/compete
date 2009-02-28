using System;
using System.Collections.Generic;
using Compete.Core.Infrastructure;
using Compete.Model;
using Compete.Model.Game;
using Compete.Model.Repositories;

namespace Compete.Site.Infrastructure
{

  public class ScoreKeeper : IScoreKeeper
  {
    readonly ILeaderboardRepository _leaderboardRepository;

    public ScoreKeeper(ILeaderboardRepository leaderboardRepository)
    {
      _leaderboardRepository = leaderboardRepository;
    }

    public void Record(IEnumerable<MatchResult> newResults)
    {
      lock (this)
      {
        Leaderboard leaderboard = _leaderboardRepository.GetLeaderboard();
        leaderboard.Include(newResults);
        _leaderboardRepository.SetLeaderboard(leaderboard);
      }
    }
  }
}
