using System;
using System.IO;
using System.Security.Policy;

namespace Compete.Site.Infrastructure
{
  public static class AppDomainHelper
  {
    static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(AppDomainHelper));
    private static string _binaryDirectory;

    public static void CopyDependencies(string to)
    {
      foreach (string path in Directory.GetFiles(Path.Combine(_binaryDirectory, "Bin"), "*.dll"))
      {
        _log.Info("Copying " + path);
        File.Copy(path, Path.Combine(to, Path.GetFileName(path)), true);
      }
    }

    public static O InSeparateAppDomain<I, O>(string applicationBase, I parameter, Func<I, O> method)
    {
      AppDomainSetup setup = new AppDomainSetup();
      Evidence evidence = AppDomain.CurrentDomain.Evidence;
      setup.ApplicationBase = applicationBase;
      AppDomain domain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), evidence, setup);
      try
      {
        Type sandboxType = typeof(Sandbox<I, O>);
        string sandboxAssemblyName = sandboxType.Assembly.GetName().Name;
        string sandboxTypeName = sandboxType.FullName;
        Sandbox<I, O> sandbox = (Sandbox<I, O>)domain.CreateInstanceAndUnwrap(sandboxAssemblyName, sandboxTypeName);
        O value = sandbox.Apply(parameter, method);
        return value;
      }
      catch (Exception error)
      {
        _log.Error(error);
        return default(O);
      }
      finally
      {
        AppDomain.Unload(domain);
      }
    }

    public static void Start(string path)
    {
      _binaryDirectory = path;
    }
  }

  public class Sandbox<I, O> : MarshalByRefObject
  {
    static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(AppDomainHelper));

    public O Apply(I parameter, Func<I, O> method)
    {
      try
      {
        return method(parameter);
      }
      catch (Exception error)
      {
        _log.Error(error);
      }
      return default(O);
    }

    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}