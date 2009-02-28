using System;
using System.Collections.Generic;
using System.Linq;

using Compete.Model.Game;

namespace Compete.Model
{
  public class Leaderboard : Entity
  {
    readonly List<MatchResult> _last = new List<MatchResult>();

    public Leaderboard()
      : base(Guid.Empty)
    {
    }

    public void Include(IEnumerable<MatchResult> toBeIncluded)
    {
      System.Diagnostics.Debug.WriteLine("Has : " + _last.Count);
      foreach (var old in _last.ToArray())
      {
        foreach (var mr in toBeIncluded)
        {
          if (mr.IsSameMatchup(mr))
          {
            _last.Remove(old);
          }
        }
      }
      _last.AddRange(toBeIncluded);
      System.Diagnostics.Debug.WriteLine("Now Has : " + _last.Count);
    }

    public TeamStandings ToStandings()
    {
      Dictionary<string, TotalTeamScore> scores = new Dictionary<string, TotalTeamScore>(); 
      foreach (var mr in _last)
      {
        if (!scores.ContainsKey(mr.TeamName1)) scores[mr.TeamName1] = new TotalTeamScore(mr.TeamName1);
        if (!scores.ContainsKey(mr.TeamName2)) scores[mr.TeamName2] = new TotalTeamScore(mr.TeamName2);
        scores[mr.TeamName1].Add(mr);
        scores[mr.TeamName2].Add(mr);
      }
      return new TeamStandings(scores.Values);
    }
  }

  public static class ScoreMappings
  {
    public static double ToTeam1Score(this MatchResult mr)
    {
      if (mr.IsTie) return 1;
      if (mr.WinnerTeamName.Equals(mr.TeamName1)) return 3;
      return 0;
    }

    public static double ToTeam2Score(this MatchResult mr)
    {
      if (mr.IsTie) return 1;
      if (mr.WinnerTeamName.Equals(mr.TeamName2)) return 3;
      return 0;
    }
  }

  public class TotalTeamScore
  {
    readonly string _teamName;
    int _wins;
    int _losses;
    int _ties;

    public int Score
    {
      get { return _ties + _wins * 3; }
    }

    public TotalTeamScore(string teamName)
    {
      _teamName = teamName;
    }

    public void Add(MatchResult mr)
    {
      if (mr.LoserTeamName.Equals(_teamName))
      {
        _losses++;
      }
      else if (mr.WinnerTeamName.Equals(_teamName))
      {
        _wins++;
      }
      else if (mr.TeamName1.Equals(_teamName) || mr.TeamName2.Equals(_teamName))
      {
        _ties++;
      }
    }
  }

  public class TeamStandings
  {
    readonly TeamStanding[] _standings;

    public TeamStandings(IEnumerable<TotalTeamScore> scores)
    {
      //scores.OrderByDescending(x => x.Score).Select()
      //var nameAndScore = scores.Select(x => new { TeamName = x.Key, Score = x.Value }).OrderByDescending(x => x.Score);
    }

    public IEnumerable<TeamStanding> Leaders()
    {
      return _standings;
    }
  }
}