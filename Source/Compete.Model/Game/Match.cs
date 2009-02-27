namespace Compete.Model.Game
{
  public class Match
  {
    readonly IPlayer _player1;
    readonly IPlayer _player2;
    readonly IGame _game;

    public Match(IGame game, IPlayer player1, IPlayer player2)
    {
      _game = game;
      _player1 = player1;
      _player2 = player2;
    }

    public GameResult Play()
    {
      var gameResult = _game.Play(_player1, _player2);
      return gameResult;
    }
  }
}