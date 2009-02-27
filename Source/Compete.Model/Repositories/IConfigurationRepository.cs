using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compete.Model.Repositories
{
  public interface IConfigurationRepository
  {
    Configuration GetConfiguration();
    void SetConfiguration(Configuration configuration);
  }
}