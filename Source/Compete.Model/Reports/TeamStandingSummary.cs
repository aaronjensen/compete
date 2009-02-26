namespace Compete.Model.Reports
{
  public class TeamStandingSummary
  {
    public string Name { get; private set; }
    public int Rank { get; private set; }
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Ties { get; private set; }
    public int LastChange { get; private set; }

    public TeamStandingSummary(string name, int rank, int wins, int losses, int ties, int lastChange)
    {
      Name = name;
      Rank = rank;
      Wins = wins;
      Losses = losses;
      Ties = ties;
      LastChange = lastChange;
    }
  }
}