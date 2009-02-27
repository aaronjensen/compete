using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Core;

namespace Compete.Model.Game
{
  public class AggregateResult
  {
    readonly Dictionary<BotPlayer, int> _playerToScoreMap;

    public AggregateResult(IEnumerable<GameResult> results)
    {
      _playerToScoreMap = new Dictionary<BotPlayer, int>();
      results.SelectMany(x => x.Players).Each(x => _playerToScoreMap[x] = 0);

      results.Each(result =>
      {
        if (result.IsTie)
        {
          result.Players.Each(player =>
          {
            _playerToScoreMap[player]++;
          });
        }
        else
        {
          _playerToScoreMap[result.Winner] += 3;
        }
      });

      var winners = _playerToScoreMap.GroupBy(x => x.Value).OrderByDescending(x => x.Key).First();

      if (winners.Count() == _playerToScoreMap.Count)
      {
        IsTie = true;
      }
      else
      {
        Winner = winners.First().Key;
      }
    }

    public IEnumerable<BotPlayer> Players
    {
      get { return _playerToScoreMap.Keys; }
    }

    public BotPlayer Winner
    {
      get; private set;
    }

    public bool IsTie
    {
      get; private set;
    }
  }
}