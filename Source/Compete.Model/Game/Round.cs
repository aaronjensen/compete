using System.Collections.Generic;
using System.Linq;
using Machine.Core;

namespace Compete.Model.Game
{
  public class RoundResult
  {
    IOrderedEnumerable<PlayerStanding> _standings;

    public RoundResult(IEnumerable<PlayerStanding> standings)
    {
      _standings = standings.OrderByDescending(x => x.Score);
    }

    public IEnumerable<IPlayer> Leaders
    {
      get
      {
        return _standings.Where(x => x.Score == _standings.First().Score).Select(x => x.Player);
      }
    }
  }

  public class PlayerStanding
  {
    readonly IPlayer _player;

    public PlayerStanding(IPlayer player)
    {
      _player = player;
    }

    public IPlayer Player
    {
      get { return _player; }
    }

    public int Score
    {
      get
      {
        return Wins * 3 + Ties;
      }
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
  }

  public class Round
  {
    readonly IEnumerable<IPlayer> _players;
    readonly IGame _game;
    List<AggregateResult> _results = new List<AggregateResult>();
    public Round(IGame game, IEnumerable<IPlayer> players)
    {
      _players = players;
      _game = game;
    }

    public IEnumerable<IPlayer> Leaders
    {
      get { return Result.Leaders; }
    }

    public RoundResult Result
    {
      get; private set;
    }

    public RoundResult Play()
    {
      var playerStandings = new Dictionary<IPlayer, PlayerStanding>();
      _players.Each(x => playerStandings[x] = new PlayerStanding(x));
      
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