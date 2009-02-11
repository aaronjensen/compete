using System.Collections.Generic;

namespace Compete.GitWrapper.Model
{
  public class FileSet : IEnumerable<string>
  {
    private readonly List<string> _paths = new List<string>();

    public bool IsEmpty
    {
      get { return _paths.Count == 0; }
    }

    public FileSet(IEnumerable<string> paths)
    {
      _paths = new List<string>(paths);
    }

    public IEnumerator<string> GetEnumerator()
    {
      return _paths.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}