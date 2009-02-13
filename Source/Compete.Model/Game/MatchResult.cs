using System;
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
      Winner = resultsGroupedByPlayer.OrderByDescending(y => y.Count()).First().Key;
    }

    public IPlayer Winner { get; private set; }
  }
}
