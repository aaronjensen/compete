using System;
using System.Collections.Generic;

using Compete.GitWrapper.Commands;

namespace Compete.GitWrapper.Model
{
  public class WorkingCopy : GitRepository
  {
    public bool IsDirty
    {
      get { return this.HasUnstagedChanges || this.HasUncomittedChanges; }
    }

    public bool HasModifiedFiles
    {
      get { return !ModifiedFiles().IsEmpty; }
    }

    public bool HasUntrackedFiles
    {
      get { return !OtherFiles().IsEmpty; }
    }

    public bool HasUncomittedChanges
    {
      get { return !DiffFiles().IsEmpty; }
    }

    public bool HasUnstagedChanges
    {
      get { return !DiffFiles().IsEmpty; }
    }

    public WorkingCopy(IHasRootDirectory directory)
     : base(directory)
    {
    }

    public WorkingCopy(string directory)
     : base(directory)
    {
    }

    public StagingArea StagingArea
    {
      get { return this.Index; }
    }

    public StagingArea Index
    {
      get { return new StagingArea(this); }
    }

    public FileSet ModifiedFiles()
    {
      return new GitListFiles().Modified(this);
    }

    public FileSet OtherFiles()
    {
      return new GitListFiles().Others(this);
    }

    public FileSet StagedFiles()
    {
      return new GitListFiles().Staged(this);
    }

    public void Commit(string message, string author)
    {
      new GitCommit().CommitStagingArea(this, message, author);
    }

    public Diff DiffFiles()
    {
      return new GitDiff().Files(this);
    }

    public Diff DiffIndex()
    {
      return new GitDiff().Index(this);
    }

    public override string ToString()
    {
      return "WorkingCopy<" + this + ">";
    }
  }
}