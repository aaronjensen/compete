using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compete.TeamManagement
{
  public interface ITeamBotCommands
  {
    void BotUpdated(string teamName);
  }

  public class TeamBotCommands : ITeamBotCommands
  {
    public void BotUpdated(string teamName)
    {
    }
  }
}
