namespace Compete.Model
{
  public class TeamStanding
  {
    public int Rank { get; private set; }
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Ties { get; private set; }
    public int LastChange { get; private set; }

    public TeamStanding(int rank, int wins, int losses, int ties, int lastChange)
    {
      Rank = rank;
      Wins = wins;
      Losses = losses;
      Ties = ties;
      LastChange = lastChange;
    }
  }
}