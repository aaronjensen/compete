using System;
using System.Collections.Generic;
using System.Reflection;

using Compete.Model;

using Db4objects.Db4o;

namespace Compete.Persistence.Repositories
{
  public class Repository<TType> where TType : Entity
  {
    readonly IObjectContainer _db;

    public Repository(IObjectContainer container)
    {
      _db = container;
    }

    public TType FindById(Guid id)
    {
      var query = _db.Query();
      query.Constrain(typeof(TType));
      query.Descend("_id").Constrain(id);

      return FindUnique(query.Execute());
    }

    protected TType FindUnique(IObjectSet set)
    {
      List<TType> found = new List<TType>();
      while (set.HasNext())
      {
        found.Add((TType)set.Next());
      }
      if (found.Count > 1)
      {
        throw new AmbiguousMatchException();
      }
      if (found.Count == 0)
      {
        return default(TType);
      }
      return found[0];
    }

    public void Add(TType obj)
    {
      _db.Store(obj);
      _db.Commit();
    }

    public void Update(TType obj)
    {
      _db.Store(obj);
      _db.Commit();
    }

    public void Remove(TType obj)
    {
      _db.Delete(obj);
      _db.Commit();
    }

    public int Count()
    {
      var query = _db.Query();
      query.Constrain(typeof(TType));

      return query.Execute().Count;
    }

    public IEnumerable<TType> GetAllOf()
    {
      return _db.Query<TType>(typeof(TType));
    } 
  }
}