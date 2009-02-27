using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Compete.Site.Models
{
  public class PlayerFileRepository
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

    public ICollection<PlayerFile> FindAll()
    {
      List<PlayerFile> playerFiles = new List<PlayerFile>();
      foreach (string path in Directory.GetFiles(_directory, "*.dll"))
      {
        playerFiles.Add(new PlayerFile(path));
      }
      return playerFiles;
    }
  }

  public class PlayerFile
  {
    readonly string _path;

    public string Path
    {
      get { return _path; }
    }

    public PlayerFile(string path)
    {
      _path = path;
    }
  }
}
