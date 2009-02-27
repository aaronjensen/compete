using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Compete.Site.Models
{
  public class AssemblyFileRepository
  {
    private const string _directory = @"C:\Compete";

    public void Add(HttpPostedFileBase file, string fileName)
    {
      if (!Directory.Exists(_directory))
      {
        Directory.CreateDirectory(_directory);
      }
      string savedFileName = Path.Combine(_directory, fileName);
      file.SaveAs(savedFileName);
    }

    public ICollection<AssemblyFile> FindAllGames()
    {
      return FindAllPlayers(Path.Combine(_directory, "Games")).ToArray();
    }

    public ICollection<AssemblyFile> FindAllPlayers()
    {
      return FindAllPlayers(_directory).ToArray();
    }

    private static IEnumerable<AssemblyFile> FindAllPlayers(string directory)
    {
      foreach (string path in Directory.GetFiles(directory, "*.dll"))
      {
        yield return new AssemblyFile(path);
      }
    }
  }

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
