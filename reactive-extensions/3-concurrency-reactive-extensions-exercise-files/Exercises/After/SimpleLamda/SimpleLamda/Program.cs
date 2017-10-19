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

using System.Threading;

#endregion

namespace SimpleLamda
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Application Thread {0}", Thread.CurrentThread.ManagedThreadId);
            var testString = "test string";
            // Use a lamda expression to create a delegate.
            // The Console.WriteLine isn't being executed here
            // it's just being "remembered" until the delegate is invoked
            Action lamda = () => Console.WriteLine("Thread {0} {1}",
                Thread.CurrentThread.ManagedThreadId, testString);
            // then invoke the delegate
            lamda();
            testString = "test string 2";
            // The technique Rx uses is to schedule a
            // delegate to be run using one of the
            // implementations of IScheduler provided
            // by the Schedule class.
            Scheduler.NewThread.Schedule(lamda);

            // When Rx needs to execute a delegate recursively it
            // uses a trampoline so that the stack will not overflow
            Action repeater = null;
            testString = "Repeater";
            // EnsureTrampoline is available only for the CurrentThread
            // so we schedule a delegate inside a delegate.
            Scheduler.NewThread.Schedule(() =>
            {
                var repeat = 3;
                // this runs on a new thread and defines a delegate named repeater
                repeater = () =>
                {
                    if (repeat <= 0) return;
                    Console.WriteLine("{0} {1}", testString, repeat);
                    repeat -= 1;
                    repeater();
                };
                // repeater is run on a trampoline using the
                // current thread, which is a new thread
                // created by the scheduler
                Scheduler.CurrentThread.EnsureTrampoline(repeater);
            });

        }
        static string OnNext(string message)
        {
            return String.Format("{0} \"{1}\"", Thread.CurrentThread.ManagedThreadId, message);
        }
    }
}
