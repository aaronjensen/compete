using System.Collections.Generic;
using System.Linq;

namespace Compete.Model.Game
{
  public class Competition
  {
    readonly IGame _game;
    readonly List<BotPlayer> _players = new List<BotPlayer>();
    readonly List<Round> _rounds = new List<Round>();

    public Competition(IGame game)
    {
      _game = game;
    }

    public IEnumerable<BotPlayer> Leaders
    {
      get
      {
        if (_rounds.Count > 0)
        {
          return _rounds.Last().Leaders;
        }
        return new BotPlayer[0];
      }
    }

    public RoundResult PlayRound()
    {
      var round = new Round(_game, _players);
      RoundResult rr = round.Play();
      _rounds.Add(round);
      return rr;
    }

    public void AddPlayer(BotPlayer player)
    {
      _players.Add(player);
    }
  }
}