using System;
using System.Collections.Generic;

namespace Compete.Model.Repositories
{
  public interface IConfigurationRepository
  {
    Configuration GetConfiguration();
    void SetConfiguration(Configuration configuration);
  }
}