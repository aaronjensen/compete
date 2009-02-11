using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Compete.Model;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;

namespace Compete.Persistence
{
  public static class Database
  {
    static IObjectContainer _db;

    public static IObjectContainer Db
    {
      get { return _db; }
    }

    public static void Start(string path)
    {
      var configuration = Db4oFactory.NewConfiguration();
      configuration.DetectSchemaChanges(true);
      configuration.AllowVersionUpdates(true);
      configuration.ActivationDepth(4);
      configuration.ObjectClass(typeof(Entity)).ObjectField("_id").Indexed(true);
      configuration.RefreshClasses();
      _db = Db4oFactory.OpenFile(configuration, Path.Combine(path, "Database.yap"));
    }
  }
}
