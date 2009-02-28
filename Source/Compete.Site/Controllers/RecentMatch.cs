using Compete.Model.Game;

namespace Compete.Site.Controllers
{
  public class RecentMatch
  {
    public string OpponentTeamName { get; private set; }
    public string Result { get; private set; }

    public RecentMatch(string teamName, MatchResult result)
    {
      if (result.TeamName1 == teamName)
      {
        OpponentTeamName = result.TeamName2;
      }
      else
      {
        OpponentTeamName = result.TeamName1;
      }

      if (result.IsTie)
      {
        Result = "Tie";
      }
      else if (result.WinnerTeamName == teamName)
      {
        Result = "Win";
      }
      else
      {
        Result = "Loss";
      }
    }
    
  }
}