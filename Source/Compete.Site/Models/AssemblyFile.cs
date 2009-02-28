using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Compete.Site.Models
{
  public class AssemblyFileRepository
  {
    public const string Directory = @"C:\Compete";

    public void Add(HttpPostedFileBase file, string fileName)
    {
      DirectoryHelpers.CreateIfNecessary(Directory);
      DirectoryHelpers.CreateIfNecessary(Directory, "Bots");
      DirectoryHelpers.CreateIfNecessary(Directory, "Games");
      string savedFileName = Path.Combine(Path.Combine(Directory, "Bots"), fileName);
      file.SaveAs(savedFileName);
    }
    
    public ICollection<AssemblyFile> FindAllGamesAndPlayers()
    {
      List<AssemblyFile> files = new List<AssemblyFile>();
      files.AddRange(FindAllGames());
      files.AddRange(FindAllPlayers());
      return files;
    }

    public ICollection<AssemblyFile> FindAllGames()
    {
      return FindAllDlls(Path.Combine(Directory, "Games")).ToArray();
    }

    public ICollection<AssemblyFile> FindAllPlayers()
    {
      return FindAllDlls(Path.Combine(Directory, "Bots")).ToArray();
    }

    private static IEnumerable<AssemblyFile> FindAllDlls(string directory)
    {
      foreach (string path in System.IO.Directory.GetFiles(directory, "*.dll"))
      {
        yield return new AssemblyFile(path);
      }
    }
  }

  public static class DirectoryHelpers
  {
    public static void CreateIfNecessary(string full)
    {
      if (!Directory.Exists(full))
      {
        Directory.CreateDirectory(full);
      }
    }
    public static void CreateIfNecessary(string path, string relative)
    {
      string full = Path.Combine(path, relative);
      CreateIfNecessary(full);
    }
  }

  [Serializable]
  public class AssemblyFile
  {
    readonly string _path;

    public string Path
    {
      get { return _path; }
    }

    public AssemblyFile(string path)
    {
      _path = path;
    }

    public bool Equals(AssemblyFile obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return Equals(obj._path, _path);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof(AssemblyFile)) return false;
      return Equals((AssemblyFile) obj);
    }

    public override Int32 GetHashCode()
    {
      return _path.GetHashCode();
    }
  }
}
