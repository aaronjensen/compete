using System;
using System.Collections.Generic;
using System.Linq;

using Compete.Model.Game;
using Compete.Model.Reports;

namespace Compete.Model
{
  public class Leaderboard : Entity
  {
    readonly List<MatchResult> _last = new List<MatchResult>();
    readonly Dictionary<string, TotalTeamScore> _scores = new Dictionary<string, TotalTeamScore>();

    public Leaderboard()
      : base(Guid.Empty)
    {
    }

    public IEnumerable<MatchResult> GetMatchResultsForTeam(string teamName)
    {
      return _last.Where(x => x.TeamName1 == teamName || x.TeamName2 == teamName);
    }

    public void Include(IEnumerable<MatchResult> toBeIncluded)
    {
      if (toBeIncluded == null) return;

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
      Refresh();
    }

    public IEnumerable<TeamStandingSummary> ToStandingSummary(IDictionary<string, string> teamNames)
    {
      List<string> remainingNames = teamNames.Keys.Select(x => x.ToLower()).ToList();
      int rank = 1;
      foreach (var group in _scores.Values.GroupBy(x => x.Score).OrderByDescending(x => x.Key))
      {
        foreach (var score in group)
        {
          remainingNames.Remove(score.TeamName.ToLower());
          yield return new TeamStandingSummary(teamNames[score.TeamName.ToLower()], rank, score.Wins, score.Losses, score.Ties, score.Score);
        }
        rank++;
      }
      foreach (string notFound in remainingNames)
      {
        yield return new TeamStandingSummary(teamNames[notFound.ToLower()]);
      }
    }

    private void Refresh()
    {
      _scores.Clear();
      foreach (var mr in _last)
      {
        if (!_scores.ContainsKey(mr.TeamName1)) _scores[mr.TeamName1] = new TotalTeamScore(mr.TeamName1);
        if (!_scores.ContainsKey(mr.TeamName2)) _scores[mr.TeamName2] = new TotalTeamScore(mr.TeamName2);
        _scores[mr.TeamName1].Add(mr);
        _scores[mr.TeamName2].Add(mr);
      }
    }

    public MatchResult GetMatchResultsForMatchBetween(string teamName, string otherTeamName)
    {
      return
        _last.Where(
          x =>
            (x.TeamName1 == teamName && x.TeamName2 == otherTeamName) ||
            (x.TeamName1 == otherTeamName && x.TeamName2 == teamName)).FirstOrDefault();
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

    public string TeamName
    {
      get { return _teamName; }
    }

    public int Wins
    {
      get { return _wins; }
    }

    public int Losses
    {
      get { return _losses; }
    }

    public int Ties
    {
      get { return _ties; }
    }

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
      if (mr.IsTie)
      {
        _ties++;
      }
      else if (mr.LoserTeamName.Equals(_teamName))
      {
        _losses++;
      }
      else if (mr.WinnerTeamName.Equals(_teamName))
      {
        _wins++;
      }
    }
  }
}