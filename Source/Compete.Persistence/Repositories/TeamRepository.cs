using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.Model;
using Compete.Model.Repositories;
using Db4objects.Db4o;

namespace Compete.Persistence.Repositories
{
  public class TeamRepository : ITeamRepository
  {
    readonly Repository<Team> _repository;

    public TeamRepository(IObjectContainer objectContainer)
    {
      _repository = new Repository<Team>(objectContainer);
    }

    public Team FindById(Guid id)
    {
      return _repository.FindById(id);
    }

    public void Add(Team team)
    {
      _repository.Add(team);
    }

    public int Count()
    {
      return _repository.Count();
    }

    public IEnumerable<Team> GetAllTeams()
    {
      return _repository.GetAllOf();
    }
  }
}
