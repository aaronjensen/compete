using System;

namespace Compete.GitWrapper.Model
{
  public class File : IEquatable<File>
  {
    private readonly string _path;

    public string Path
    {
      get { return _path; }
    }

    public File(string path)
    {
      _path = path;
    }

    public bool Equals(File file)
    {
      if (file == null) return false;
      return Equals(_path, file._path);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(this, obj)) return true;
      return Equals(obj as File);
    }

    public override Int32 GetHashCode()
    {
      return _path.GetHashCode();
    }

    public override string ToString()
    {
      return "File<" + _path + ">";
    }
  }
}