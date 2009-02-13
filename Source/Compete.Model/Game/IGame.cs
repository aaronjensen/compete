namespace Compete.Model.Game
{
  public interface IGame<TGame>
  {
    GameResult<TGame> Play(IPlayer<TGame> player1, IPlayer<TGame> player2);
  }
}