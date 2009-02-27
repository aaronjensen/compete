using System.Collections.Generic;
using System.Linq;

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

    public IEnumerable<IPlayer> Leaders
    {
      get
      {
        if (_rounds.Count > 0)
          return _rounds.Last().Leaders;

        return new IPlayer[0];
      }
    }

    public void PlayRound()
    {
      var round = new Round(_game, _players);
      round.Play();
      _rounds.Add(round);
    }

    public void AddPlayer(IPlayer player)
    {
      _players.Add(player);
    }
  }
}