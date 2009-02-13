using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compete.Model.Game
{
  public class MatchResult
  {
    public MatchResult(IEnumerable<GameResult> results)
    {
      var resultsGroupedByPlayer = results.GroupBy(x => x.Winner);
      var playersInDescendingOrder = resultsGroupedByPlayer.OrderByDescending(y => y.Count());

      /*
      if (playersInDescendingOrder.ElementAt(0))
      Winner = .First().Key;
       */
      Players = results.SelectMany(r => r.Players);
    }

    public IPlayer Winner { get; private set; }

    public IEnumerable<IPlayer> Players
    {
      get; private set;
    }

    public bool IsTie
    {
      get { throw new NotImplementedException(); }
    }
  }
}
