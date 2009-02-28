using System;
using System.Collections.Generic;

namespace Compete.Model.Game
{
  public class GameResult
  {
    public string Log
    { 
      get; private set;
    }

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

    public static GameResult Tie(BotPlayer player1, BotPlayer player2, string log)
    {
      return new GameResult
      {
        Players = new[] {player1, player2},
        IsTie = true,
        Log = log
      };
    }

    public static GameResult WinnerAndLoser(BotPlayer winner, BotPlayer loser, string log)
    {
      return new GameResult
      {
        Winner = winner, 
        Loser = loser,
        Players = new[] {winner, loser},
        Log = log
      };
    }
  }
}