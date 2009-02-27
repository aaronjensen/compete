using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.Model.Game;

namespace Compete.SampleGame
{
  /*
  public class GuessANumberBelowTenOrSomethingBitch : IGame
  {
    public GameResult Play(IPlayer player1, IPlayer player2)
    {
      var random = new Random();
      var number = random.Next(10);
      var state = new GameState();

      var guess1 = player1.MakeDecision(state) as MyBestGuess ?? new MyBestGuess(9999999);
      var guess2 = player2.MakeDecision(state) as MyBestGuess ?? new MyBestGuess(9999999);

      int distance1 = Math.Abs(guess1.GuessedNumber - number);
      int distance2 = Math.Abs(guess2.GuessedNumber - number);

      if (distance1 == distance2)
      {
        return GameResult.Tie(new [] { player1, player2 });
      }
      else if (distance1 < distance2)
      {
        return GameResult.WinnerAndLoser(player1, player2);
      }
      else
      {
        return GameResult.WinnerAndLoser(player2, player1);
      }
    }
  }

  public class MyBestGuess : GameDecision
  {
    readonly int _guessedNumber;

    public MyBestGuess(int guessedNumber)
    {
      _guessedNumber = guessedNumber;
    }

    public int GuessedNumber
    {
      get { return _guessedNumber; }
    }
  }*/
}
