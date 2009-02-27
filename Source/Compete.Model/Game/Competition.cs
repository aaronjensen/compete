using System;
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

    public IEnumerable<MatchResult> PlayRound(IEnumerable<string> teamNames)
    {
      var teams = _players.Where(x => teamNames.Contains(x.TeamName));

      return PlayRound(teams);
    }

    IEnumerable<MatchResult> PlayRound(IEnumerable<BotPlayer> teams)
    {
      if (_players.Count == 0)
      {
        throw new InvalidOperationException("No players in game!");
      }

      var round = new Round(_game, _players, teams);
      var matchResults = round.Play();
      _rounds.Add(round);

      return matchResults;
    }

    public void AddPlayer(BotPlayer player)
    {
      _players.Add(player);
    }
  }
}