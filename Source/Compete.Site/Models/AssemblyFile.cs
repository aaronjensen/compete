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
      string savedFileName = Path.Combine(Directory, fileName);
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
      return FindAllPlayers(Path.Combine(Directory, "Games")).ToArray();
    }

    public ICollection<AssemblyFile> FindAllPlayers()
    {
      return FindAllPlayers(Path.Combine(Directory, "Bots")).ToArray();
    }

    private static IEnumerable<AssemblyFile> FindAllPlayers(string directory)
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
  }
}
