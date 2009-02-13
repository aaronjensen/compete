using System.Collections.Generic;

namespace Compete.Model.Game
{
  public class Competition
  {
    readonly IGame _game;
    readonly List<IPlayer> _players;
    readonly List<Round> _rounds;

    public Competition(IGame game)
    {
      _game = game;
      _players = new List<IPlayer>();
    }

    public void PlayRound()
    {
      var round = new Round(_game, _players);
      round.Play();
    }

    public void AddPlayer(IPlayer player)
    {
      _players.Add(player);
    }
  }
}