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

namespace BackgroundForegroundThreads
{
    class Program
    {
        static void Main()
        {
            // write out thread application is running on
            Console.WriteLine("Application\tThread: {0}", Thread.CurrentThread.ManagedThreadId);
            // make a sequence of numbers
            var numbers = from number in new int[] { 1, 2, 3 } select number;
            // turn it into an observable sequence of numbers that is processed asynchronously
            var observableNumbers = numbers.ToObservable(Scheduler.ThreadPool);
            // subscribe and run callbacks
            var d =observableNumbers.Subscribe(Output, Oops, ImDone);
            Console.WriteLine("Good Bye!");
            Console.ReadKey();

        }
        // methods to use as delegate for processing observableNumbers
        // and keeping track of thread they were run on

        // processes value by writing it along with thread id
        static void Output(int number)
        {
            Console.WriteLine("Value: {0}\tThread: {1}\tBackground {2}", number,
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground);
        }
        // process error by writing message and thread id
        static void Oops(Exception exception)
        {
            Console.WriteLine("Message: {0}\tThread: {1}",
                exception.Message, Thread.CurrentThread.ManagedThreadId);
        }
        // processes completion by writing thread id 
        static void ImDone()
        {
            Console.WriteLine("I'm done on thread {0}", Thread.CurrentThread.ManagedThreadId);
        }
        // method used to simulate work requiring time
        static int Slow(int number)
        {
            Console.WriteLine("snooooooze");
            Thread.Sleep(2000);
            return number;
        }

    }
}
