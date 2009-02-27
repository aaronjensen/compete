using System;
using System.Collections.Generic;
using System.Reflection;

namespace Compete.Site.Infrastructure
{
  public class DynamicAssemblyTypeFinder
  {
    readonly List<Assembly> _assemblies = new List<Assembly>();

    public void AddAssembly(string assemblyPath)
    {
      Assembly assembly = Assembly.LoadFrom(assemblyPath);
      _assemblies.Add(assembly);
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
