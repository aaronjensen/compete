using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.Model.Game;

namespace Compete.Core.Infrastructure
{
  public interface IScoreKeeper
  {
    void Record(IEnumerable<MatchResult> newResults);
  }
}
