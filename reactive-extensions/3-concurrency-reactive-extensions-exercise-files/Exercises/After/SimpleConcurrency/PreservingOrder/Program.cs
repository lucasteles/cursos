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

using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

#endregion

namespace PreservingOrder
{
    class SimultaneousDelegatesCheck : IDisposable
    {
        private static int _simultaneousDelegateCount = 0;
        public void Dispose()
        {
            Interlocked.Decrement(ref _simultaneousDelegateCount);
        }
        public SimultaneousDelegatesCheck()
        {
            var usage = 0;
            if ((usage = Interlocked.Increment(ref _simultaneousDelegateCount)) != 1)
            {
                // if another delegate has started but not completed it's work
                // this _simultaneousDelegateCount will not be 1 at this point
                Console.WriteLine("{0} delegates are running concurrently", usage);
            }
            
        }

    }
    class Program
    {
        // use to track how many subscription delegates are running at once
        static void Main()
        {
            // write out thread application is running on
            Console.WriteLine("Application\tThread: {0}", Thread.CurrentThread.ManagedThreadId);
            // make a sequence of numbers
            var numbers = from number in new int[] { 1, 2, 3, 4, 5, 6, 7, 9 , 10, 12 }
                          select Slow(number);
            // turn it into an observable sequence of numbers that is processed asynchronously
            var observableNumbers = numbers.ToObservable(Scheduler.NewThread);
            //var observableNumbers = numbers.ToObservable().SubscribeOn(Scheduler.NewThread).ObserveOn(Scheduler.NewThread);
            // subscribe and run callbacks
            observableNumbers.Subscribe(Output, Oops, ImDone);
            // just to confirm our test will detect simulaneous usage
#if false
            Observable.Start(() =>
            {
                Thread.Sleep(3500);
                using (new SimultaneousDelegatesCheck())
                {
                    Console.WriteLine("Nemisis is passing");
                    Thread.Sleep(2000);
                }
                Console.WriteLine("Nemisis has passed");
            });
#endif
            Console.ReadKey();

        }

        // method used to simulate work requiring time
        static int Slow(int number)
        {
             Console.WriteLine("slow work");
             Thread.Sleep(900);
             return number;
        }

        // methods to use as delegate for processing observableNumbers
        // and keeping track of thread they were run on


        // processes value by writing it along with thread id
        static void Output(int number)
        {
            using(new SimultaneousDelegatesCheck())
            {
                Console.WriteLine("Output Snooooozing");
                Thread.Sleep(1000);
                Console.WriteLine("Value: {0}\tThread: {1}", number,
                                  Thread.CurrentThread.ManagedThreadId);
            }
        }
        // process error by writing message and thread id
        static void Oops(Exception exception)
        {
            using (new SimultaneousDelegatesCheck())
            {
                Console.WriteLine("Message: {0}\tThread: {1}",
                exception.Message, Thread.CurrentThread.ManagedThreadId);
            }
        }
        // processes completion by writing thread id 
        static void ImDone()
        {
            using (new SimultaneousDelegatesCheck())
            {
                Console.WriteLine("I'm done on thread {0}",
                    Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}
