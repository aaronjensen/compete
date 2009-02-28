using System;
using System.Collections.Generic;

namespace Compete.Model.Repositories
{
  public interface ILeaderboardRepository
  {
    Leaderboard GetLeaderboard();
    void SetLeaderboard(Leaderboard configuration);
  }
}