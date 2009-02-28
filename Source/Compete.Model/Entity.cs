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
  }
}
