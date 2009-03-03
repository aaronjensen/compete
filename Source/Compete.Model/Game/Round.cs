using System;
using System.Collections.Generic;
using System.Linq;

using Machine.Core;

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
      return String.Join(", ",_standings.Select(x => x.ToString()).ToArray());
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
    readonly IEnumerable<BotPlayer> _allPlayers;
    readonly IEnumerable<BotPlayer> _playersToEvaluate;
    readonly IGame _game;

    public Round(IGame game, IEnumerable<BotPlayer> allPlayers, IEnumerable<BotPlayer> playersToEvaluate)
    {
      _allPlayers = allPlayers;
      _playersToEvaluate = playersToEvaluate;
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
          foreach (BotPlayer player in _allPlayers)
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

    public IEnumerable<MatchResult> Play()
    {
      var playerStandings = new Dictionary<BotPlayer, TeamStanding>();
      _allPlayers.Each(x => playerStandings[x] = new TeamStanding(x.TeamName));

      foreach (var player in _allPlayers)
      {
        System.Diagnostics.Debug.WriteLine("Player: " + player.TeamName);
      }

      var matchResults = new List<MatchResult>();

      for (int i = 0; i < _allPlayers.Count() - 1; ++i)
      {
        for (int j = i + 1; j < _allPlayers.Count(); ++j)
        {
          var player1 = _allPlayers.ElementAt(i);
          var player2 = _allPlayers.ElementAt(j);

          if (!_playersToEvaluate.Contains(player1) && !_playersToEvaluate.Contains(player2))
          {
            System.Diagnostics.Debug.WriteLine("Skipping: " + player1.TeamName + " " + player2.TeamName);
            continue;
          }

          var match = new Match(_game, player1, player2);

          var result = match.Play();

          matchResults.Add(result.ToMatchResult());
        }
      }

      return matchResults;
    }
  }

  public static class ResultMapping
  {
    public static MatchResult ToMatchResult(this GameResult gameResult)
    {
      if (gameResult.IsTie)
      {
        return MatchResult.Tie(gameResult.Players.First().TeamName, gameResult.Players.Last().TeamName, gameResult.Log);
      }

      return MatchResult.WinnerAndLoser(gameResult.Winner.TeamName, gameResult.Loser.TeamName, gameResult.Log);
    }
  }
}