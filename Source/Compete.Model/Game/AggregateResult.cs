using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Core;

namespace Compete.Model.Game
{
  public class AggregateResult
  {
    public AggregateResult(IEnumerable<GameResult> results)
    {
      var playerToScoreMap = new Dictionary<IPlayer, int>();
      results.SelectMany(x => x.Players).Each(x => playerToScoreMap[x] = 0);

      results.Each(result =>
      {
        if (result.IsTie)
        {
          result.Players.Each(player =>
          {
            playerToScoreMap[player]++;
          });
        }
        else
        {
          playerToScoreMap[result.Winner] += 3;
        }
      });

      //playerToScoreMap.OrderByDescending(x => x.Value)
    }

    //public IEnumerable<PlayerStanding> 

    public IPlayer Winner
    {
      get; private set;
    }

    public bool IsTie
    {
      get; private set;
    }
  }
}