using System.Collections.Generic;

namespace Compete.Model.Game
{
  public class Competition
  {
    readonly IGame _game;
    readonly List<IPlayer> _players = new List<IPlayer>();
    readonly List<Round> _rounds = new List<Round>();

    public Competition(IGame game)
    {
      _game = game;
    }

    public IPlayer Leader
    {
      get;
      private set;
    }

    public void PlayRound()
    {
      var round = new Round(_game, _players);
      round.Play();
      _rounds.Add(round);
      Leader = round.Winner;
    }

    public void AddPlayer(IPlayer player)
    {
      _players.Add(player);
    }
  }
}