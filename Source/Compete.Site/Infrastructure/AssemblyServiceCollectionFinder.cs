using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Compete.Site.Infrastructure
{
  public class AssemblyServiceCollectionFinder
  {
    readonly List<Assembly> _assemblies = new List<Assembly>();
    readonly string _rootPath;

    public AssemblyServiceCollectionFinder()
    {
      _rootPath = System.IO.Path.GetDirectoryName(typeof(AssemblyServiceCollectionFinder).Assembly.Location);
    }

    public void AddAllAssemblies(string path)
    {
      foreach (string assemblyPath in Directory.GetFiles(path, "*.dll"))
      {
        Assembly assembly = Assembly.LoadFrom(assemblyPath);
        _assemblies.Add(assembly);
      }
    }

    public IEnumerable<T> Create<T>()
    {
      foreach (Type type in EnumerateTypesOf<T>())
      {
        yield return (T)Activator.CreateInstance(type);
      }
    }

    private IEnumerable<Type> EnumerateTypesOf<T>()
    {
      foreach (Assembly assembly in _assemblies)
      {
        foreach (Type type in assembly.GetExportedTypes())
        {
          if (typeof(T).IsAssignableFrom(type))
          {
            yield return type;
          }
        }
      }
    }
  }
}
