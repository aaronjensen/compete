using System;
using System.Collections.Generic;
using Compete.Bot;

namespace Compete.Bot
{
  public interface IBotFactory
  {
    IBot CreateBot();
  }
}