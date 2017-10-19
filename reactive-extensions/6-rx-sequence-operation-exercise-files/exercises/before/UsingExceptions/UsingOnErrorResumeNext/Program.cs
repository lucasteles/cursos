using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace UsingOnErrorResumeNext
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("main thd {0}", Thread.CurrentThread.ManagedThreadId);
            var done = new ManualResetEvent(false);
            var query = from number in Enumerable.Range(0, 10) select number;
            var sequence = query.ToObservable(Scheduler.NewThread);
            sequence

            done.WaitOne();
            Console.WriteLine("I'm done");
        }
    }
}
