using System.Collections.Generic;
using System.Linq;

namespace Compete.Model.Game
{
  public class Round
  {
    readonly IEnumerable<IPlayer> _players;
    readonly IGame _game;
    List<MatchResult> _results = new List<MatchResult>();
    public Round(IGame game, IEnumerable<IPlayer> players)
    {
      _players = players;
      _game = game;
    }

    public IPlayer Winner
    {
      get; private set;      
    }

    public void Play()
    {
      
      for (int i = 0; i < _players.Count(); ++i)
      {
        for (int j = i + 1; j < _players.Count(); ++j)
        {
          var player1 = _players.ElementAt(i);
          var player2 = _players.ElementAt(j);

          var match = new Match(_game, player1, player2);

          var result = match.Play();
          
          _results.Add(result);

          Winner = CalculateWinner(_results);
        }
      }

    }

    IPlayer CalculateWinner(List<MatchResult> results)
    {
      var playerToScoreMap = new Dictionary<IPlayer, int>();
      foreach (var player in _players)
        playerToScoreMap.Add(player, 0);
      
      foreach (var result in results)
      {
        if(result.IsTie)
        {
          foreach (var player in result.Players)
          {
            playerToScoreMap[player]++;
          }
        }
        else
        {
          playerToScoreMap[result.Winner] += 3;
        }
        
      }

      return null;
    }
  }
}