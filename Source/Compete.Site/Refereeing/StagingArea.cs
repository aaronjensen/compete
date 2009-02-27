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

    public string Root
    {
      get { return _root; }
    }

    public StagingArea(IEnumerable<AssemblyFile> files)
    {
      _root = Path.Combine(@"C:\Compete\Staging", Guid.NewGuid().ToString("D"));
      DirectoryHelpers.CreateIfNecessary(_root);
      AppDomainHelper.CopyDependencies(_root);
      foreach (AssemblyFile file in files)
      {
        File.Copy(file.Path, Path.Combine(_root, Path.GetFileName(file.Path)), true);
      }
    }

    public void Dispose()
    {
      Directory.Delete(_root, true);
    }
  }
}