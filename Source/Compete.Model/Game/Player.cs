namespace Compete.Model.Game
{
  public interface IPlayer
  {
    GameDecision MakeDecision(GameState state);
  }

  public class GameDecision
  {
  }
}