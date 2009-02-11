using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Compete.GitWrapper.Utilities
{
  public class CommandLineApplication
  {
    private readonly string _workingDirectory;
    private readonly string _executable;
    private readonly string _argv;

    public CommandLineApplication(string executable, string argv, string workingDirectory)
    {
      _executable = executable;
      _argv = argv;
      _workingDirectory = workingDirectory;
    }

    public CommandLineOutput Run()
    {
      ProcessStartInfo startInfo = new ProcessStartInfo(_executable, _argv);
      startInfo.WorkingDirectory = _workingDirectory;
      startInfo.UseShellExecute = false;
      startInfo.RedirectStandardOutput = true;
      startInfo.RedirectStandardError = true;
      Process process = Process.Start(startInfo);
      if (process == null)
      {
        throw new InvalidOperationException();
      }
      string standardError = process.StandardError.ReadToEnd();
      string standardOut = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      return new CommandLineOutput((short)process.ExitCode, standardOut, standardError);
    }
  }
  public class CommandLineOutput
  {
    private readonly short _exitCode;
    private readonly string _standardOut;
    private readonly string _standardError;

    public short ExitCode
    {
      get { return _exitCode; }
    }

    public string StandardOut
    {
      get { return _standardOut; }
    }

    public string StandardError
    {
      get { return _standardError; }
    }

    public bool HasFailureStatus
    {
      get { return _exitCode != 0; }
    }

    public CommandLineOutput(short exitCode, string standardOut, string standardError)
    {
      _exitCode = exitCode;
      _standardOut = standardOut;
      _standardError = standardError;
    }
  }
}
