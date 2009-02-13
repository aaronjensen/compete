using System.Collections.Generic;
using System.Linq;

namespace Compete.Model.Game
{
  public class Round<TGame>
  {
    readonly IEnumerable<IPlayer<TGame>> _players;
    readonly IGame<TGame> _game;

    public Round(IGame<TGame> game, IEnumerable<IPlayer<TGame>> players)
    {
      _players = players;
      _game = game;
    }

    public void Play()
    {
      for (int i = 0; i < _players.Count(); ++i)
      {
        for (int j = i + 1; j < _players.Count(); ++j)
        {
          var player1 = _players.ElementAt(i);
          var player2 = _players.ElementAt(j);

          var match = new Match<TGame>(_game, player1, player2);

          match.Play();
        }
      }

    }
  }
}