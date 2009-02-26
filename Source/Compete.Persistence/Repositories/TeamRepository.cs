using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.Model;
using Compete.Model.Repositories;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace Compete.Persistence.Repositories
{
  public class TeamRepository : ITeamRepository
  {
    readonly IObjectContainer _objectContainer;
    readonly Repository<Team> _repository;

    public TeamRepository(IObjectContainer objectContainer)
    {
      _objectContainer = objectContainer;
      _repository = new Repository<Team>(objectContainer);
    }

    public Team FindById(Guid id)
    {
      return _repository.FindById(id);
    }

    public Team FindByTeamName(string teamName)
    {
      var result = from Team t in _objectContainer
        where t.Name == teamName
        select t;

      return result.FirstOrDefault();
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
