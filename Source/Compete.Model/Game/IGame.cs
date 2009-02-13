namespace Compete.Model.Game
{
  public interface IGame
  {
    GameResult Play(IPlayer player1, IPlayer player2);
  }
}