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
      var configuration = _repository.FindById(Guid.Empty);
      
      return configuration;
    }

    public void SetConfiguration(Configuration configuration)
    {
      _repository.Update(configuration);
    }
  }
}
