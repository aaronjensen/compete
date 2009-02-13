using System.Collections.Generic;

namespace Compete.Model.Game
{
  public class Competition<TGame>
  {
    readonly IGame<TGame> _game;
    readonly List<IPlayer<TGame>> _players;
    readonly List<Round<TGame>> _rounds;

    public Competition(IGame<TGame> game)
    {
      _game = game;
      _players = new List<IPlayer<TGame>>();
    }

    public void PlayRound()
    {
      var round = new Round<TGame>(_game, _players);
      round.Play();
    }

    public void AddPlayer(IPlayer<TGame> player)
    {
      _players.Add(player);
    }
  }
}