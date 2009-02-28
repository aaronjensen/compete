using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Compete.Uploader
{
  public class Program
  {
    // {url} {file} {teamName} {password}
    static void Main(string[] args)
    {
      if (args.Length != 4)
      {
        Console.WriteLine("Usage: Compete.Uploader {url} {dll} {teamName} {password}");
        Environment.Exit(1);
      }

      Console.WriteLine("Uploading...");
      var client = new WebClient();
      
      try
      {
        client.UploadFile(String.Format("{0}/UploadBot?teamName={1}&password={2}", args[0], args[2], args[3]), "post", args[1]);
      }
      catch (Exception err)
      {
        Console.WriteLine("Couldn't upload, check your username and password");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.Error.WriteLine("FAIL: " + err);
        Environment.Exit(1);
      }

      Console.WriteLine("Done, Good Luck!");
    }
  }
}