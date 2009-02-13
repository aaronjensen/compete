namespace Compete.Model.Game
{
  public interface IPlayer<TGame>
  {
    GameAction<TGame> MakeDecision(GameState<TGame> state);
  }

  public class GameAction<T>
  {
  }
}