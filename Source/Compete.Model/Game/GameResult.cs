namespace Compete.Model.Game
{
  public class GameResult
  {
    public IPlayer Winner
    {
      get; private set;
    }

    public IPlayer Loser
    {
      get; private set;
    }

    public bool WasTie
    {
      get; private set;
    }

    public static GameResult Tie()
    {
      return new GameResult
      {
        WasTie = true
      };
    }

    public static GameResult WinnerLoser(IPlayer winner, IPlayer loser)
    {
      return new GameResult
      {
        Winner = winner, 
        Loser = loser
      };
    }
  }
}