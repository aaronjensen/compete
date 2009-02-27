using System;
using System.Collections.Generic;

using Compete.Model.Game;

namespace Compete.Model.Repositories
{
  public interface ILeaderboardRepository
  {
    Leaderboard GetLeaderboard();
    void SetLeaderboard(Leaderboard configuration);
  }
}