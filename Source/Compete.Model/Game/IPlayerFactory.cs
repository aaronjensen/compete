using System;
using System.Collections.Generic;

namespace Compete.Model.Game
{
  public interface IPlayerFactory
  {
    IPlayer CreatePlayer();
  }
}
