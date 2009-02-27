using System;
using System.Collections.Generic;

namespace Compete.Model.Game
{
  public class GameResult
  {
    public BotPlayer Winner
    {
      get; private set;
    }

    public BotPlayer Loser
    {
      get; private set;
    }

    public bool IsTie
    {
      get; private set;
    }

    public IEnumerable<BotPlayer> Players
    {
      get; private set;
    }

    public static GameResult Tie(params BotPlayer[] players)
    {
      return new GameResult
      {
        Players = players,
        IsTie = true
      };
    }

    public static GameResult WinnerAndLoser(BotPlayer winner, BotPlayer loser)
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