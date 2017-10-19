#region PluralsightExerciseExample
// Copyright Pluralsight LLC 2011
// 
// This example is for the exclusive use of subscribers to a Pluralsight On Demand!
// subscription that includes example code.
// 
// Please do not post these examples or pass them around.
// 
// This code is meant to be used for training purposes only.
// 
// No representations are made as to its usefullness or correctness.
// 
// This code may not be used in any applications or production code.
// 
// Thank You!
#endregion

#region

using System;
using System.Concurrency;
using System.Linq;
using System.Threading;

#endregion

namespace ImmediateVsCurrentThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application Thread {0}", Thread.CurrentThread.ManagedThreadId);
            var observableQuery = (from number in Enumerable.Range(1, 1) select number)
                .ToObservable(Scheduler.Immediate).Repeat().Take(5);
            observableQuery.Subscribe(n => 
                Console.WriteLine("Value {0}\tThread {1}", 
                n, Thread.CurrentThread.ManagedThreadId),
                () =>Console.WriteLine("Done"));
            Console.WriteLine("Application Finished");
            
        }
    }
}
