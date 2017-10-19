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

namespace TerminatingSubscription
{
    class Program
    {
        static void Main(string[] args)
        {
            var query = from number in Enumerable.Range(1, 10) select Slow(number);
            var observableQuery = query.ToObservable(Scheduler.NewThread);
            // keep reference to object returned by subcribe
            var subscription = observableQuery.
                Subscribe(Console.WriteLine,
                e =>Console.WriteLine(e.Message),
                () => Console.WriteLine("Done"));
            Console.ReadKey();
            // once key has been press dispose subscription
            subscription.Dispose();
            Console.WriteLine("Subscription Disposed");
            Console.ReadKey();

        }

        private static int Slow(int number)
        {
            Thread.Sleep(1000);
            return number;
        }
    }
}
