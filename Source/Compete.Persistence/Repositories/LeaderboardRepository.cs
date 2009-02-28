using System;
using System.Collections.Generic;

using Compete.Model;
using Compete.Model.Repositories;

using Db4objects.Db4o;

namespace Compete.Persistence.Repositories
{
  public class LeaderboardRepository : ILeaderboardRepository
  {
    readonly Repository<Leaderboard> _repository;

    public LeaderboardRepository(IObjectContainer objectContainer)
    {
      _repository = new Repository<Leaderboard>(objectContainer);
    }

    public Leaderboard GetLeaderboard()
    {
      var value = _repository.FindById(Guid.Empty);
      if (value == null)
      {
        value = new Leaderboard();
        _repository.Add(value);
      }

      return value;
    }

    public void SetLeaderboard(Leaderboard leaderboard)
    {
      _repository.Update(leaderboard);
    }
  }
}
