using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;



namespace VidSimpleExample
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Thread {0}", Thread.CurrentThread.ManagedThreadId);
      var query = from number in Enumerable.Range(1,5)
                  select number;
      foreach (var number in query)
      {
        Console.WriteLine(number);
      }
      ImDone();
      var observableQuery = query.ToObservable();
      observableQuery.Subscribe(Console.WriteLine, ImDone);
                              observableQuery = query.ToObservable(Scheduler.NewThread);
      var observer = Observer.Create<int>(ProcessNumber);
      observableQuery.Subscribe(observer);
      Console.ReadKey();
    }
    static void ProcessNumber(int number)
    {
      Console.WriteLine("{0} Thread {1}", number, Thread.CurrentThread.ManagedThreadId);
    }
    static void ImDone()
    {
      Console.WriteLine("I'm done!");
    }
  }
}
