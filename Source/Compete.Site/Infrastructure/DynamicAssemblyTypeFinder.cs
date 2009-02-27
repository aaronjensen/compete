using System;
using System.Collections.Generic;
using System.Reflection;

using Compete.Site.Models;

namespace Compete.Site.Infrastructure
{
  public class DynamicAssemblyTypeFinder
  {
    static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DynamicAssemblyTypeFinder));
    readonly List<Assembly> _assemblies = new List<Assembly>();

    public void AddAssembly(AssemblyFile assemblyFile)
    {
      Assembly assembly = Assembly.LoadFile(assemblyFile.Path);
      _assemblies.Add(assembly);
    }

    public void AddAll(ICollection<AssemblyFile> files)
    {
      foreach (AssemblyFile file in files)
      {
        AddAssembly(file);
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
          if (typeof(T).IsAssignableFrom(type) && !type.IsAbstract)
          {
            yield return type;
          }
        }
      }
    }
  }
}
