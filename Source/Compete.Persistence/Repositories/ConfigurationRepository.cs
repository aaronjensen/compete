using System;
using System.Collections.Generic;

using Compete.Model;
using Compete.Model.Repositories;

using Db4objects.Db4o;

namespace Compete.Persistence.Repositories
{
  public class ConfigurationRepository : IConfigurationRepository
  {
    readonly Repository<Configuration> _repository;

    public ConfigurationRepository(IObjectContainer objectContainer)
    {
      _repository = new Repository<Configuration>(objectContainer);
    }

    public Configuration GetConfiguration()
    {
      return _repository.FindById(Guid.Empty);
    }

    public void SetConfiguration(Configuration configuration)
    {
      var currentConfiguration = GetConfiguration();
      if (currentConfiguration != null)
      {
        _repository.Remove(currentConfiguration);
      }

      _repository.Add(configuration);
    }
  }
}
