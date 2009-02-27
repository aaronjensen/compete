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
      if (!System.IO.Directory.Exists(Directory))
      {
        System.IO.Directory.CreateDirectory(Directory);
      }
      string savedFileName = Path.Combine(Directory, fileName);
      file.SaveAs(savedFileName);
    }

    public ICollection<AssemblyFile> FindAllGames()
    {
      return FindAllPlayers(Path.Combine(Directory, "Games")).ToArray();
    }

    public ICollection<AssemblyFile> FindAllPlayers()
    {
      return FindAllPlayers(Directory).ToArray();
    }

    private static IEnumerable<AssemblyFile> FindAllPlayers(string directory)
    {
      foreach (string path in System.IO.Directory.GetFiles(directory, "*.dll"))
      {
        yield return new AssemblyFile(path);
      }
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
