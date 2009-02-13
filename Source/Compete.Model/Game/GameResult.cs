namespace Compete.Model.Game
{
  public class GameResult<TGame>
  {
    readonly IPlayer<TGame> _player1;
    readonly IPlayer<TGame> _player2;

    public IPlayer<TGame> Winner
    {
      get { return _player1; }
    }
    
    public IPlayer<TGame> Loser
    {
      get { return _player1; }
    }

    public bool WasTie
    {
      get { return false; }
    }
  }
}