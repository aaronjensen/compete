using System;
using System.Threading;

namespace Compete.Site.Refereeing
{
  public interface IRefereeThread
  {
    bool IsRunning { get; }
    bool Start(Referee referee);
  }

  public class RefereeThread : IRefereeThread
  {
    static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(RefereeThread));
    Thread _thread;
    Referee _currentlyRunning;

    public bool IsRunning
    {
      get { return _currentlyRunning != null; }
    }

    public bool Start(Referee referee)
    {
      if (_currentlyRunning != null)
      {
        _log.Info("Not starting, already running...");
        return false;
      }

      _thread = new Thread(Main);
      _log.Info("Starting:");
      _currentlyRunning = referee;
      _thread.Start(this);
      return true;
    }

    protected virtual void Run()
    {
      _currentlyRunning.StartRound();
      _currentlyRunning = null;
    }

    private static void Main(object parameter)
    {
      ((RefereeThread)parameter).Run();
    }
  }
}