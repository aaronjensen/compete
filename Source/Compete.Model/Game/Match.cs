namespace Compete.Model.Game
{
  public class Match<TGame>
  {
    readonly IPlayer<TGame> _player1;
    readonly IPlayer<TGame> _player2;
    readonly IGame<TGame> _game;

    public Match(IGame<TGame> game, IPlayer<TGame> player1, IPlayer<TGame> player2)
    {
      _game = game;
      _player1 = player1;
      _player2 = player2;
    }

    public MatchResult<TGame> Play()
    {
      var gameResult = _game.Play(_player1, _player2);
      return new MatchResult<TGame>(new [] { gameResult });
    }
  }
}