using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Concurrency;


namespace VidSimpleExample
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Thread {0}", Thread.CurrentThread.ManagedThreadId);
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
