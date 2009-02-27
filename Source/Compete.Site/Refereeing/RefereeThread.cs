using System;
using System.Threading;

namespace Compete.Site.Refereeing
{
  public class RefereeThread
  {
    readonly Thread _thread = new Thread(Main);
    readonly Referee _referee;

    public RefereeThread(Referee referee)
    {
      _referee = referee;
    }

    public void Start()
    {
      _thread.Start(this);
    }

    protected virtual void Run()
    {
      _referee.Start();
    }

    private static void Main(object parameter)
    {
      ((RefereeThread)parameter).Run();
    }
  }
}