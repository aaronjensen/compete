using System;
using System.Collections.Generic;

using Compete.GitWrapper.Utilities;
using Compete.GitWrapper.Model;

namespace Compete.GitWrapper.Commands
{
  public class GitCommand
  {
    private readonly string _executable = @"C:\Program Files (x86)\Git\bin\git.exe";

    protected GitOutput Run(IHasRootDirectory directory, string command, params string[] argv)
    {
      string args = command + " " + argv.Join(" ");
      CommandLineApplication application = new CommandLineApplication(_executable, args, directory.Root);
      CommandLineOutput output = application.Run();
      if (output.HasFailureStatus)
      {
        throw new InvalidOperationException(output.StandardError);
      }
      return new GitOutput(output.StandardOut);
    }
  }
  public class GitInit : GitCommand
  {
    public void Initialize(GitRepository repository)
    {
      Run(repository, "init");
    }
  }
  public class GitClone : GitCommand
  {
    public void Existing(GitRepository repository, string origin)
    {
      Run(new IsTemporaryDirectory(), "clone", origin, repository.Root);
    }
  }
  public class GitListFiles : GitCommand
  {
    public FileSet Modified(WorkingCopy wc)
    {
      return new FileSet(Run(wc, "ls-files", "--modified").SplitAndTrimLines());
    }

    public FileSet Others(WorkingCopy wc)
    {
      return new FileSet(Run(wc, "ls-files", "--others").SplitAndTrimLines());
    }

    public FileSet Staged(WorkingCopy wc)
    {
      return new FileSet(Run(wc, "ls-files", "--stage").SelectColumn(3));
    }

    public FileSet All(WorkingCopy wc)
    {
      return new FileSet(Run(wc, "ls-files").SplitAndTrimLines());
    }
  }
  public class GitAdd : GitCommand
  {
    public void Stage(WorkingCopy wc, File file)
    {
      Run(wc, "add", file.Path);
    }
  }
  public class GitRemove : GitCommand
  {
    public void Unstage(WorkingCopy wc, File file)
    {
      Run(wc, "rm", "--cached", file.Path);
    }
  }
  public class GitCommit : GitCommand
  {
    public void CommitStagingArea(WorkingCopy wc, string message, string author)
    {
      Run(wc, "commit", "-m", message.Quote(), "--author", author.Quote());
    }
  }
  public class GitReset : GitCommand
  {
    public void Hard(WorkingCopy wc)
    {
      Run(wc, "reset", "--hard");
    }

    public void File(WorkingCopy wc, File file, string treeish)
    {
      Run(wc, "reset", treeish, file.Path);
    }
  }
  public class GitDiff : GitCommand
  {
    public class GitRawDiffTransformer : GitOutputTransformer<DiffEntry>
    {
      public GitRawDiffTransformer()
      {
        Add(@"\d+\s+\d+\s+(?<HB>\S+)\s+(?<HA>\S+)\s+M\s+(?<PATH>\S+)", (line, m) => { return new ModifiedFile(new File(m.Groups["PATH"].Value)); });
        Add(@"\d+\s+\d+\s+(?<HB>\S+)\s+(?<HA>\S+)\s+A\s+(?<PATH>\S+)", (line, m) => { return new AddedFile(new File(m.Groups["PATH"].Value)); });
        Add(@"\d+\s+\d+\s+(?<HB>\S+)\s+(?<HA>\S+)\s+D\s+(?<PATH>\S+)", (line, m) => { return new DeletedFile(new File(m.Groups["PATH"].Value)); });
      }
    }

    public Diff Index(WorkingCopy wc)
    {
      return new Diff(Run(wc, "diff", "--raw", "--cached", "--abbrev=40").Transform(new GitRawDiffTransformer()));
    }

    public Diff Files(WorkingCopy wc)
    {
      return new Diff(Run(wc, "diff", "--raw", "--abbrev=40").Transform(new GitRawDiffTransformer()));
    }
  }
  public class GitFetch : GitCommand
  {
    public void Remote(GitRepository repository, string remote)
    {
      Run(repository, "fetch", remote);
    }

    public void All(GitRepository repository)
    {
      Run(repository, "fetch", "-v");
    }
  }
  public class GitMerge : GitCommand
  {
    public void From(WorkingCopy wc, Branch branch)
    {
      Run(wc, "merge", branch.Name);
    }
  }
  public class GitPush : GitCommand
  {
    public void To(WorkingCopy wc, string remote)
    {
      Run(wc, "push", remote);
    }
  }
  public class GitCheckout : GitCommand
  {
    public void Branch(WorkingCopy wc, Branch branch)
    {
      Run(wc, "checkout", branch.Name);
    }
  }
  public class GitBranches : GitCommand
  {
    public class GitBranchesTransformer : GitOutputTransformer<Branch>
    {
      public GitBranchesTransformer()
      {
        Add(@"(\*.)?(?<NAME>\S+)\s+(\S+)\s+(?<MESSAGE>\S+)", (line, m) => { return new Branch(m.Groups["NAME"].Value, m.Groups["MESAGE"].Value); });
      }
    }

    public IEnumerable<Branch> All(GitRepository repository)
    {
      return Run(repository, "branch", "-av").Transform(new GitBranchesTransformer());
    }
  }
}