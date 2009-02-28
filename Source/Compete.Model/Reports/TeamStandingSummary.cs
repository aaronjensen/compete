namespace Compete.Model.Reports
{
  public class TeamStandingSummary
  {
    public string Name { get; private set; }
    public int Rank { get; private set; }
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Ties { get; private set; }
    public int Score { get; private set; }

    public TeamStandingSummary(string name)
      : this(name, -1, 0, 0, 0, 0)
    {
    }

    public TeamStandingSummary(string name, int rank, int wins, int losses, int ties, int score)
    {
      Name = name;
      Rank = rank;
      Wins = wins;
      Losses = losses;
      Ties = ties;
      Score = score;
    }
  }
}