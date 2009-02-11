using System;

namespace Compete.GitWrapper.Model
{
  public interface IHasRootDirectory
  {
    string Root
    {
      get;
    }
  }
  public class IsTemporaryDirectory : IHasRootDirectory
  {
    public string Root
    {
      get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); }
    }
  }
}