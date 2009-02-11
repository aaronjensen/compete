using System;
using System.Collections.Generic;

using Compete.GitWrapper.Commands;

namespace Compete.GitWrapper.Model
{
  public class GitRepository : IHasRootDirectory
  {
    private readonly string _directory;

    public string Root
    {
      get { return _directory; }
    }

    public GitRepository(IHasRootDirectory directory)
     : this(directory.Root)
    {
    }

    public GitRepository(string directory)
    {
      _directory = directory;
    }

    public void Initialize()
    {
      new GitInit().Initialize(this);
    }

    public void Clone(IHasRootDirectory origin)
    {
      new GitClone().Existing(this, origin.Root);
    }

    public void FetchAll()
    {
      new GitFetch().All(this);
    }

    public IEnumerable<Branch> Branches
    {
      get { return new GitBranches().All(this); }
    }
  }
}