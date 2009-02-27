namespace Compete.Model.Game
{
  public class BotPlayer
  {
    readonly string _teamName;
    readonly IBot _bot;

    public string TeamName
    {
      get { return _teamName; }
    }

    public IBot Bot
    {
      get { return _bot; }
    }

    public BotPlayer(string teamName, IBot bot)
    {
      _teamName = teamName;
      _bot = bot;
    }
  }
}