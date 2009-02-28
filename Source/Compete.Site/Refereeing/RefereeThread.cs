using System;
using System.Collections.Generic;
using System.Threading;

namespace Compete.Site.Refereeing
{
  public interface IRefereeThread
  {
    bool IsRunning { get; }
    bool Start(RoundParameters parameters);
  }

  public class RefereeThread : IRefereeThread
  {
    static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(RefereeThread));
    readonly Queue<RoundParameters> _queue = new Queue<RoundParameters>();
    Thread _thread;
    Referee _currentlyRunning;

    public bool IsRunning
    {
      get { return _currentlyRunning != null; }
    }

    public bool Start(RoundParameters parameters)
    {
      if (_currentlyRunning != null)
      {
        _queue.Enqueue(parameters);
        _log.Info("Not starting, already running...");
        return false;
      }
      _thread = new Thread(Main);
      _log.Info("Starting:");
      _currentlyRunning = new Referee(parameters);
      _thread.Start(this);
      return true;
    }

    protected virtual void Run()
    {
      try
      {
        _currentlyRunning.StartRound();
      }
      finally
      {
        _currentlyRunning = null;
        if (_queue.Count > 0)
        {
          _log.Info("Starting queued round...");
          Start(RoundParameters.Merge(_queue.ToArray()));
          _queue.Clear();
        }
      }
    }

    private static void Main(object parameter)
    {
      ((RefereeThread)parameter).Run();
    }
  }
}