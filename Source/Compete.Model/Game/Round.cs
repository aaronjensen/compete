using System;
using System.Collections.Generic;
using System.Linq;

using Machine.Core;

using Compete.Core;

namespace Compete.Model.Game
{
  [Serializable]
  public class RoundResult
  {
    readonly TeamStanding[] _standings;

    public TeamStanding[] Standings
    {
      get { return _standings; }
    }

    public RoundResult(IEnumerable<TeamStanding> standings)
    {
      _standings = standings.OrderByDescending(x => x.Score).ToArray();
    }

    public override string ToString()
    {
      return _standings.Select(x => x.ToString()).Join(", ");
    }
  }

  [Serializable]
  public class TeamStanding
  {
    readonly string _name;

    public TeamStanding(string name)
    {
      _name = name;
    }

    public string Name
    {
      get { return _name; }
    }

    public int Score
    {
      get { return Wins * 3 + Ties; }
    }

    public int Wins
    {
      get; set;
    }

    public int Losses
    {
      get; set;
    }

    public int Ties
    {
      get; set;
    }

    public override string ToString()
    {
      return _name + ": " + this.Score;
    }
  }

  public class Round
  {
    readonly IEnumerable<BotPlayer> _players;
    readonly IGame _game;

    public Round(IGame game, IEnumerable<BotPlayer> players)
    {
      _players = players;
      _game = game;
    }

    private IEnumerable<string> LeaderTeamNames
    {
      get { return Result.Standings.Where(x => x.Score == Result.Standings.First().Score).Select(x => x.Name); }
    }

    public IEnumerable<BotPlayer> Leaders
    {
      get
      {
        foreach (string name in LeaderTeamNames)
        {
          foreach (BotPlayer player in _players)
          {
            if (player.TeamName == name)
            {
              yield return player;
            }
          }
        }
      }
    }

    public RoundResult Result
    {
      get; private set;
    }

    public RoundResult Play()
    {
      var playerStandings = new Dictionary<BotPlayer, TeamStanding>();
      _players.Each(x => playerStandings[x] = new TeamStanding(x.TeamName));
      
      for (int i = 0; i < _players.Count(); ++i)
      {
        for (int j = i + 1; j < _players.Count(); ++j)
        {
          var player1 = _players.ElementAt(i);
          var player2 = _players.ElementAt(j);

          var match = new Match(_game, player1, player2);

          var result = match.Play();

          foreach (var player in new [] {player1, player2})
          {
            if (result.Winner == player)
              playerStandings[player].Wins++;
            else if (result.Loser == player)
              playerStandings[player].Losses++;
            else if (result.IsTie)
              playerStandings[player].Ties++;
          }
        }
      }

      Result = new RoundResult(playerStandings.Values);

      return Result;
    }
  }
}