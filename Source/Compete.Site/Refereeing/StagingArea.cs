using System;
using System.Collections.Generic;
using System.IO;
using Compete.Site.Infrastructure;
using Compete.Site.Models;

namespace Compete.Site.Refereeing
{
  public class StagingArea : IDisposable
  {
    readonly string _root;
    readonly List<AssemblyFile> _stagedAssemblies = new List<AssemblyFile>();

    public string Root
    {
      get { return _root; }
    }

    public AssemblyFile[] StagedAssemblies
    {
      get { return _stagedAssemblies.ToArray(); }
    }

    public StagingArea(IEnumerable<AssemblyFile> files)
    {
      _root = Path.Combine(@"C:\Compete\Staging", Guid.NewGuid().ToString("D"));
      DirectoryHelpers.CreateIfNecessary(_root);
      AppDomainHelper.CopyDependencies(_root);
      foreach (AssemblyFile file in files)
      {
        var stagedFile = new AssemblyFile(Path.Combine(_root, Path.GetFileName(file.Path)));
        _stagedAssemblies.Add(stagedFile);
        File.Copy(file.Path, stagedFile.Path, true);
      }
    }

    public void Dispose()
    {
      Directory.Delete(_root, true);
    }
  }
}