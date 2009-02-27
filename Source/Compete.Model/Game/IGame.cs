namespace Compete.Model.Game
{
  public interface IGame
  {
    GameResult Play(BotPlayer player1, BotPlayer player2);
  }
}