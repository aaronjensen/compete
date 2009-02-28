using System;
using System.Collections.Generic;

namespace Compete.Model
{
  public class Entity
  {
    readonly Guid _id;

    public Entity()
      : this(Guid.NewGuid())
    {
    }

    public Entity(Guid id)
    {
      _id = id;
    }

    public bool Equals(Entity obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj._id.Equals(_id);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return Equals((Entity) obj);
    }

    public override int GetHashCode()
    {
      return _id.GetHashCode();
    }
  }
}
