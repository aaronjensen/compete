using System;
using System.Collections.Generic;

namespace Compete.Model.Game
{
  public class GameResult
  {
    public IPlayer Winner
    {
      get; private set;
    }

    public IPlayer Loser
    {
      get; private set;
    }

    public bool IsTie
    {
      get; private set;
    }

    public IEnumerable<IPlayer> Players
    {
      get; private set;
    }

    public static GameResult Tie(params IPlayer[] players)
    {
      return new GameResult
      {
        Players = players,
        IsTie = true
      };
    }

    public static GameResult WinnerAndLoser(IPlayer winner, IPlayer loser)
    {
      return new GameResult
      {
        Winner = winner, 
        Loser = loser,
        Players = new[] {winner, loser}
      };
    }
  }
}