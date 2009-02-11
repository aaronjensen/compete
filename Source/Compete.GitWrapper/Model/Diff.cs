using System.Collections.Generic;

namespace Compete.GitWrapper.Model
{
  public class Diff : IEnumerable<DiffEntry>
  {
    private readonly List<DiffEntry> _entries;

    public bool IsEmpty
    {
      get { return _entries.Count == 0; }
    }

    public Diff(IEnumerable<DiffEntry> entries)
    {
      _entries = new List<DiffEntry>(entries);
    }

    public IEnumerator<DiffEntry> GetEnumerator()
    {
      return _entries.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }

  public abstract class DiffEntry
  {
    private readonly File _file;

    public File File
    {
      get { return _file; }
    }

    protected DiffEntry(File file)
    {
      _file = file;
    }
  }

  public class ModifiedFile : DiffEntry
  {
    public ModifiedFile(File file)
      : base(file)
    {
    }

    public override string ToString()
    {
      return "Modified<" + this.File + ">";
    }
  }

  public class AddedFile : DiffEntry
  {
    public AddedFile(File file)
      : base(file)
    {
    }

    public override string ToString()
    {
      return "Added<" + this.File + ">";
    }
  }

  public class DeletedFile : DiffEntry
  {
    public DeletedFile(File file)
      : base(file)
    {
    }

    public override string ToString()
    {
      return "Deleted<" + this.File + ">";
    }
  }
}