using System;
using System.Collections.Generic;

using Compete.GitWrapper.Commands;

namespace Compete.GitWrapper.Model
{
  public class StagingArea
  {
    private readonly WorkingCopy _wc;

    public StagingArea(WorkingCopy workingCopy)
    {
      _wc = workingCopy;
    }

    public bool IsEmpty
    {
      get { return _wc.DiffIndex().IsEmpty; }
    }

    public void Add(File file)
    {
      new GitAdd().Stage(_wc, file);
    }

    public void Remove(File file)
    {
      new GitRemove().Unstage(_wc, file);
    }

    public void AddEverything()
    {
      foreach (DiffEntry entry in _wc.DiffFiles())
      {
        Add(entry.File);
      }
    }
  }
}