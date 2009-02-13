﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.Model.Repositories;
using Compete.Persistence.Repositories;
using Db4objects.Db4o;
using Machine.Container;
using Machine.Container.Plugins;

namespace Compete.Persistence
{
  public class PersistenceServices : IServiceCollection
  {
    public void RegisterServices(ContainerRegisterer register)
    {
      register.Type<ITeamRepository>().ImplementedBy<TeamRepository>();
      register.Type<IObjectContainer>().Is(Database.Db);
    }
  }
}